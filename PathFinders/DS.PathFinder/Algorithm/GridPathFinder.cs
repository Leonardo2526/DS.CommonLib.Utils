//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  gustavo_franco@hotmail.com
//
//  Copyright (C) 2006 Franco, Gustavo 
//
#define DEBUGON

using DS.PathSearch;
using DS.PathSearch.GridMap;
using DS.ClassLib.VarUtils.Points;
using FrancoGustavo.CLZ;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Media3D;
using DS.ClassLib.VarUtils.Collisions;
using FrancoGustavo.Algorithm;

namespace FrancoGustavo
{
    [Author("Franco, Gustavo")]
    class PathFinder : IPathFinder
    {
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public unsafe static extern bool ZeroMemory(int* destination, int length);

        #region Variables Declaration
        private int[,,] mGrid = null;
        private readonly ITraceCollisionDetector _collisionDetector;
        private readonly IPointConverter _pointConverter;
        private PriorityQueueB<PathFinderNode> mOpen = new PriorityQueueB<PathFinderNode>(new ComparePFNode());
        private List<PathFinderNode> mClose = new List<PathFinderNode>();
        private bool mStop = false;
        private bool mStopped = true;
        private int mHoriz = 0;
        private HeuristicFormula mFormula = HeuristicFormula.Manhattan;
        private bool mDiagonals = false;
        private int mHEstimate = 2;
        private bool mPunishChangeDirection = false;
        private bool mReopenCloseNodes = true;
        private bool mTieBreaker = false;
        private bool mHeavyDiagonals = false;
        private int mSearchLimit = 50000;
        private double mCompletedTime = 0;
        private bool mDebugProgress = false;
        private bool mDebugFoundPath = false;
        private bool mCompactPath = true;
        private byte mANGlength;
        private byte mCLZdist;
        private sbyte[,] mClearanceZone;
        #endregion

        #region Constructors
        public PathFinder(int[,,] grid, IPathRequiment pathRequiment,
            ITraceCollisionDetector collisionDetector,
           IPointConverter pointConverter)
        {
            if (grid == null)
                throw new Exception("Grid cannot be null");

            mGrid = grid;
            _collisionDetector = collisionDetector;
            _pointConverter = pointConverter;
            mCLZdist = pathRequiment.Clearance;
            mANGlength = pathRequiment.MinAngleDistance;
        }
        #endregion

        #region Properties
        public bool Stopped
        {
            get { return mStopped; }
        }

        public HeuristicFormula Formula
        {
            get { return mFormula; }
            set { mFormula = value; }
        }

        public bool Diagonals
        {
            get { return mDiagonals; }
            set { mDiagonals = value; }
        }

        public bool HeavyDiagonals
        {
            get { return mHeavyDiagonals; }
            set { mHeavyDiagonals = value; }
        }

        public int HeuristicEstimate
        {
            get { return mHEstimate; }
            set { mHEstimate = value; }
        }

        public bool PunishChangeDirection
        {
            get { return mPunishChangeDirection; }
            set { mPunishChangeDirection = value; }
        }

        public bool ReopenCloseNodes
        {
            get { return mReopenCloseNodes; }
            set { mReopenCloseNodes = value; }
        }

        public bool TieBreaker
        {
            get { return mTieBreaker; }
            set { mTieBreaker = value; }
        }

        public int SearchLimit
        {
            get { return mSearchLimit; }
            set { mSearchLimit = value; }
        }

        public double CompletedTime
        {
            get { return mCompletedTime; }
            set { mCompletedTime = value; }
        }

        public bool DebugProgress
        {
            get { return mDebugProgress; }
            set { mDebugProgress = value; }
        }

        public bool DebugFoundPath
        {
            get { return mDebugFoundPath; }
            set { mDebugFoundPath = value; }
        }
        #endregion

        #region Methods
        public void FindPathStop()
        {
            mStop = true;
        }

        public List<PathFinderNode> FindPath(Location start, Location end, sbyte[,] direction)
        {
            PathFinderNode parentNode;
            bool found = false;
            int gridX = mGrid.GetUpperBound(0) + 1;
            int gridY = mGrid.GetUpperBound(1) + 1;
            int gridZ = mGrid.GetUpperBound(2) + 1;

            mStop = false;
            mStopped = false;
            mOpen.Clear();
            mClose.Clear();

            parentNode.G = 0;
            parentNode.H = mHEstimate;
            parentNode.F = parentNode.G + parentNode.H;
            parentNode.X = start.X;
            parentNode.Y = start.Y;
            parentNode.Z = start.Z;
            parentNode.PX = parentNode.X;
            parentNode.PY = parentNode.Y;
            parentNode.PZ = parentNode.Z;
            parentNode.Id = 0;
            parentNode.ANX = start.X;
            parentNode.ANY = start.Y;
            parentNode.ANZ = start.Z;

            //Clearance zone creation
            CLZChecker cLZChecker = null;
            if (mCLZdist != 0)
            {
                //Clearance zone creation
                RefCLZCreator refCLZCreator = new RefCLZCreator(mCLZdist);
                mClearanceZone = refCLZCreator.Create();
                cLZChecker = new CLZChecker(mGrid, mClearanceZone);
            }

            mOpen.Push(parentNode);
            while (mOpen.Count > 0 && !mStop)
            {
                parentNode = mOpen.Pop();

                if (parentNode.X == end.X && parentNode.Y == end.Y && parentNode.Z == end.Z)
                {
                    mClose.Add(parentNode);
                    found = true;
                    break;
                }

                if (mClose.Count > mSearchLimit)
                {
                    mStopped = true;
                    return null;
                }

                if (mPunishChangeDirection)
                    mHoriz = (parentNode.X - parentNode.PX);

                //specify available direction for nodes (successors)
                Point3D anglePoint = new Point3D(parentNode.ANX, parentNode.ANY, parentNode.ANZ);
                Point3D parentNodePoint = new Point3D(parentNode.X, parentNode.Y, parentNode.Z);
                var distToANP = anglePoint.DistanceTo(parentNodePoint);
                Vector3D dir = (parentNodePoint - anglePoint);
                if(dir.Length !=0)
                {dir.Normalize();}
                dir = dir.ConvertToSByte();

                sbyte[,] availableDirections = distToANP == 0 || distToANP >= mANGlength ?
                     direction :
                     new sbyte[1, 3] { { Convert.ToSByte(dir.X), Convert.ToSByte(dir.Y), Convert.ToSByte(dir.Z) } };

                //Lets calculate each successors
                for (int i = 0; i <= (mDiagonals ? 8 : availableDirections.GetUpperBound(0)); i++)
                {
                    PathFinderNode newNode;

                    Vector3D nodeDir = new Vector3D(availableDirections[i, 0], availableDirections[i, 1], availableDirections[i, 2]);

                    newNode.X = parentNode.X + availableDirections[i, 0];
                    newNode.Y = parentNode.Y + availableDirections[i, 1];
                    newNode.Z = parentNode.Z + availableDirections[i, 2];
                    newNode.Id = 0;


                    if (newNode.X < 0 || newNode.Y < 0 || newNode.Z < 0 || newNode.X >= gridX || newNode.Y >= gridY || newNode.Z >= gridZ)
                        continue;

                    int newG;
                    if (mHeavyDiagonals && i > 3)
                        newG = parentNode.G + (int)(mGrid[newNode.X, newNode.Y, newNode.Z] * 2.41);
                    else
                        newG = parentNode.G + mGrid[newNode.X, newNode.Y, newNode.Z];

                    if (mCompactPath)
                    {
                        int cost = 2;
                        if ((start.X == end.X) && (newNode.X - start.X) != 0)
                        {
                            if ((newNode.Y - parentNode.Y) != 0)
                                newG += cost;

                        }
                        if (start.Y == end.Y && (newNode.Y - start.Y) != 0)
                        {
                            if ((newNode.X - parentNode.X) != 0)
                                newG += cost;

                        }
                        if (start.Z == end.Z && (newNode.Z - start.Z) != 0)
                        {
                            if ((newNode.X - parentNode.X) != 0 || (newNode.Y - parentNode.Y) != 0)
                                newG += cost;
                        }
                    }

                    if (mPunishChangeDirection)
                    {
                        newG += PunishChangeDirectionChecker.GetNewG(newNode.X, newNode.Y, newNode.Z,
                            parentNode, start, newG);

                    }



                    int foundInOpenIndex = -1;
                    for (int j = 0; j < mOpen.Count; j++)
                    {
                        if (mOpen[j].X == newNode.X && mOpen[j].Y == newNode.Y && mOpen[j].Z == newNode.Z)
                        {
                            foundInOpenIndex = j;
                            break;
                        }
                    }
                    if (foundInOpenIndex != -1 && mOpen[foundInOpenIndex].G <= newG)
                        continue;

                    int foundInCloseIndex = -1;
                    for (int j = 0; j < mClose.Count; j++)
                    {
                        if (mClose[j].X == newNode.X && mClose[j].Y == newNode.Y && mClose[j].Z == newNode.Z)
                        {
                            foundInCloseIndex = j;
                            break;
                        }
                    }
                    if (foundInCloseIndex != -1 && (mReopenCloseNodes || mClose[foundInCloseIndex].G <= newG))
                        continue;


                    //set ANP for new node
                    Point3D ANP = dir.X == nodeDir.X && dir.Y == nodeDir.Y && dir.Z == nodeDir.Z ?
                        anglePoint :
                        parentNodePoint;

                    newNode.PX = parentNode.X;
                    newNode.PY = parentNode.Y;
                    newNode.PZ = parentNode.Z;
                    newNode.G = newG;
                    newNode.ANX = Convert.ToInt32(ANP.X);
                    newNode.ANY = Convert.ToInt32(ANP.Y);
                    newNode.ANZ = Convert.ToInt32(ANP.Z);

                    var passValue = mGrid[newNode.X, newNode.Y, newNode.Z];
                    if (passValue == 0)
                    {
                        if (newNode.X == 5 && newNode.Y == 0)
                        {

                        }
                        //check collisions
                        var xyzParentNode = _pointConverter.ConvertToUCS1(new Point3D(parentNode.X, parentNode.Y, parentNode.Z));
                        var xyzNewNode = _pointConverter.ConvertToUCS1(new Point3D(newNode.X, newNode.Y, newNode.Z));
                        _collisionDetector.GetCollisions(xyzParentNode, xyzNewNode);
                        if (_collisionDetector.Collisions.Count > 0)
                        { mGrid[newNode.X, newNode.Y, newNode.Z] = 1; continue; } //unpassable point
                        else { mGrid[newNode.X, newNode.Y, newNode.Z] = 2; } // passable point
                    }
                    else if (passValue == 1) { continue; }                    

                    switch (mFormula)
                    {
                        default:
                        case HeuristicFormula.Manhattan:
                            newNode.H = mHEstimate * (Math.Abs(newNode.X - end.X) + Math.Abs(newNode.Y - end.Y) + Math.Abs(newNode.Z - end.Z));
                            break;
                        case HeuristicFormula.MaxDXDY:
                            newNode.H = mHEstimate * (Math.Max(Math.Abs(newNode.X - end.X), Math.Abs(newNode.Y - end.Y)));
                            break;
                        case HeuristicFormula.DiagonalShortCut:
                            int h_diagonal = Math.Min(Math.Abs(newNode.X - end.X), Math.Abs(newNode.Y - end.Y));
                            int h_straight = (Math.Abs(newNode.X - end.X) + Math.Abs(newNode.Y - end.Y));
                            newNode.H = (mHEstimate * 2) * h_diagonal + mHEstimate * (h_straight - 2 * h_diagonal);
                            break;
                        case HeuristicFormula.Euclidean:
                            newNode.H = (int)(mHEstimate * Math.Sqrt(Math.Pow((newNode.X - end.X), 2) + Math.Pow((newNode.Y - end.Y), 2)));
                            break;
                        case HeuristicFormula.EuclideanNoSQR:
                            newNode.H = (int)(mHEstimate * (Math.Pow((newNode.X - end.X), 2) + Math.Pow((newNode.Y - end.Y), 2)));
                            break;
                        case HeuristicFormula.Custom1:
                            Location dxy = new Location(Math.Abs(end.X - newNode.X), Math.Abs(end.Y - newNode.Y), 0);
                            int Orthogonal = Math.Abs(dxy.X - dxy.Y);
                            int Diagonal = Math.Abs(((dxy.X + dxy.Y) - Orthogonal) / 2);
                            newNode.H = mHEstimate * (Diagonal + Orthogonal + dxy.X + dxy.Y);
                            break;
                    }
                    if (mTieBreaker)
                    {
                        int dx1 = parentNode.X - end.X;
                        int dy1 = parentNode.Y - end.Y;
                        int dz1 = parentNode.Z - end.Z;
                        int dx2 = start.X - end.X;
                        int dy2 = start.Y - end.Y;
                        int dz2 = start.Z - end.Z;
                        int cross = Math.Abs(dx1 * dy2 - dx2 * dy1); //fix z here
                        newNode.H = (int)(newNode.H + cross * 0.001);
                    }
                    newNode.F = newNode.G + newNode.H;
                    mOpen.Push(newNode);
                }



                mClose.Add(parentNode);
            }

            if (found)
            {
                PathFinderNode fNode = mClose[mClose.Count - 1];
                for (int i = mClose.Count - 1; i >= 0; i--)
                {
                    if (fNode.PX == mClose[i].X && fNode.PY == mClose[i].Y && fNode.PZ == mClose[i].Z || i == mClose.Count - 1)
                    {
                        fNode = mClose[i];
                    }
                    else
                        mClose.RemoveAt(i);
                }
                mStopped = true;
                return mClose;
            }
            mStopped = true;
            return null;
        }
        #endregion

        #region Inner Classes
        [Author("Franco, Gustavo")]
        internal class ComparePFNode : IComparer<PathFinderNode>
        {
            #region IComparer Members
            public int Compare(PathFinderNode x, PathFinderNode y)
            {
                if (x.F > y.F)
                    return 1;
                else if (x.F < y.F)
                    return -1;
                return 0;
            }
            #endregion
        }
        #endregion

    }
}