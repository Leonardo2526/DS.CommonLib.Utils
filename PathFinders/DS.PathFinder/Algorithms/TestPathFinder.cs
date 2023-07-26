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

using DS.ClassLib.VarUtils.Points;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Media3D;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils;
using System.Linq;
using FrancoGustavo.Algorithm;
using System.Threading;
using System.Diagnostics;
using Rhino.Geometry;
using System.Windows.Media;
using Transform = Rhino.Geometry.Transform;

namespace FrancoGustavo
{
    [Author("Franco, Gustavo")]
    public class TestPathFinder
    {
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public unsafe static extern bool ZeroMemory(int* destination, int length);

        #region Variables Declaration
        private readonly Point3D _upperBound;
        private readonly Point3D _lowerBound;
        private readonly ITraceCollisionDetector _collisionDetector;
        private readonly INodeBuilder _nodeBuilder;
        private readonly IPoint3dConverter _pointConverter;
        private readonly List<Transform> _transforms;
        private readonly List<Transform> _reverseTransforms;
        private PriorityQueueB<PointPathFinderNode> mOpen = new PriorityQueueB<PointPathFinderNode>(new ComparePFNode());
        private List<PointPathFinderNode> mClose = new List<PointPathFinderNode>();
        private List<PointPathFinderNode> _passableNodes = new List<PointPathFinderNode>();
        private List<PointPathFinderNode> _unpassableNodes = new List<PointPathFinderNode>();
        private bool mStop = false;
        private bool mStopped = true;
        private int mHoriz = 0;
        private HeuristicFormula mFormula = HeuristicFormula.Manhattan;
        private bool mDiagonals = false;
        private int mHEstimate = 1;
        private bool mPunishChangeDirection = false;
        private bool mReopenCloseNodes = true;
        private bool mTieBreaker = false;
        private bool mHeavyDiagonals = false;
        private int mSearchLimit = 50000;
        private double mCompletedTime = 0;
        private bool mDebugProgress = false;
        private bool mDebugFoundPath = false;
        private bool mCompactPath = true;
        private double mANGlength;
        private List<int> _punishAngles;

        /// <summary>
        /// Precision to store data.
        /// </summary>
        private int _fractPrec;

        private readonly IPointVisualisator<Point3D> _pointVisualisator;
        private static int _tolerance = 2;
        private static double _dTolerance = Math.Pow(0.1, _tolerance);

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="upperBound"></param>
        /// <param name="lowerBound"></param>
        /// <param name="pathRequiment"></param>
        /// <param name="collisionDetector"></param>
        /// <param name="nodeBuilder"></param>
        /// <param name="fractPrec">Precision to store data.</param>
        /// <param name="pointVisualisator"></param>
        public TestPathFinder(Point3D upperBound, Point3D lowerBound, double minAngleDistance,
            ITraceCollisionDetector collisionDetector, INodeBuilder nodeBuilder, IPoint3dConverter pointConverter,
            int fractPrec = 7,
            IPointVisualisator<Point3D> pointVisualisator = null)
        {
            _fractPrec = fractPrec;
            _pointVisualisator = pointVisualisator;
            _upperBound = upperBound.Round(_fractPrec);
            _lowerBound = lowerBound.Round(_fractPrec);
            _collisionDetector = collisionDetector;
            _nodeBuilder = nodeBuilder;
            _pointConverter = pointConverter;
            mANGlength = Math.Round(minAngleDistance, _fractPrec);
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

        public List<int> PunishAngles
        {
            get { return _punishAngles; }
            set { _punishAngles = value; }
        }

        public CancellationTokenSource TokenSource { get; set; }

        #endregion

        #region Methods      

        public List<PointPathFinderNode> FindPath(Point3D start, Point3D end, List<Vector3D> vectors)
        {
            start = start.Round(_fractPrec);
            end = end.Round(_fractPrec);

            var directions = new List<Vector3D>();
            foreach (var dir in vectors)
            { directions.Add(dir.Round(_fractPrec)); }

            var parentNode = new PointPathFinderNode(start, start, start, _pointVisualisator, _pointConverter);

            bool found = false;
            mOpen.Clear();
            mClose.Clear();


            mOpen.Push(parentNode);
            while (mOpen.Count > 0)
            {
                if (TokenSource is not null && TokenSource.Token.IsCancellationRequested)
                { Debug.WriteLine("Path search time is up."); return null; }
                //return null;
                parentNode = mOpen.Pop();
                var point3d = _pointConverter.ConvertToUCS1(parentNode.Point.Convert()).Convert();
                _pointVisualisator?.Show(point3d);

                if (parentNode.Point.Round(_tolerance) == end.Round(_tolerance))

                // if (b &&
                //parentNode.LengthToANP >= mANGlength)
                {
                    parentNode.Point = end;
                    mClose.Add(parentNode);
                    found = true;
                    break;
                }

                if (mClose.Count > mSearchLimit)
                { break; }

                //specify available direction for nodes (successors)
                var distToANP = parentNode.LengthToANP;
                List<Vector3D> availableDirections = distToANP == 0 || distToANP >= mANGlength ?
                   directions : parentNode.DirList;

                //Lets calculate each successors
                for (int i = 0; i < availableDirections.Count; i++)
                {
                    Vector3D nodeDir = availableDirections[i];
                    var angle = Math.Round(Vector3D.AngleBetween(nodeDir, parentNode.Dir));
                    if (angle > 90 ||
                        _punishAngles is not null && _punishAngles.Any(a => a == (int)angle))
                    { continue; }

                    var newNode = _nodeBuilder.BuildWithPoint(parentNode, nodeDir);

                    if (newNode.Point.IsLess(_lowerBound) || newNode.Point.IsGreater(_upperBound)) 
                    { continue; }

                    var mOpenInd = mOpen.InnerList.IndexOf(newNode);
                    var mCloseInd = mClose.IndexOf(newNode);

                    if (mOpenInd != -1 || mCloseInd != -1) { continue; }

                    newNode = _nodeBuilder.BuildWithParameters();

                    if (mOpenInd != -1 && mOpen[mOpenInd].G <= newNode.G ||
                        mCloseInd != -1 && (mReopenCloseNodes || mClose[mCloseInd].G <= newNode.G))
                    { continue; }

                    if (newNode.Point.Round(_tolerance) == end.Round(_tolerance)
                        && newNode.LengthToANP < mANGlength
                        )
                    { continue; }

                    //collisions check
                    if (_unpassableNodes.Contains(newNode)) { continue; }
                    else
                    {
                        if (!_passableNodes.Contains(newNode))
                        {
                            //check collisions 
                            var parentPoint3D = _pointConverter.ConvertToUCS1(parentNode.Point.Convert()).Convert();
                            var newNodePoint3D = _pointConverter.ConvertToUCS1(newNode.Point.Convert()).Convert();
                            _collisionDetector.GetCollisions(parentPoint3D, newNodePoint3D);
                            if (_collisionDetector.Collisions.Count > 0)
                            { _unpassableNodes.Add(newNode); continue; } //unpassable point
                            else { _passableNodes.Add(newNode); } // passable point                            
                        }
                    }

                    mOpen.Push(newNode);
                }

                mClose.Add(parentNode);
            }

            var path = RestorePath(found, mClose);
            return path;
        }


        private List<PointPathFinderNode> RestorePath(bool found, List<PointPathFinderNode> closeNodes)
        {
            var path = new List<PointPathFinderNode>();

            if (!found) { return path; }

            path.AddRange(closeNodes);
            var fNode = path[path.Count - 1];
            for (int i = path.Count - 1; i >= 0; i--)
            {
                if (fNode.Parent == path[i].Point || i == path.Count - 1)
                {
                    fNode = path[i];
                }
                else
                { path.RemoveAt(i); }
            }

            return path;
        }

        private Point3d TransformPoint(Point3d point, List<Transform> transforms)
        {
            foreach (var transform in transforms)
            {
                point.Transform(transform);
            }

            return point;
        }

        #endregion

    }
}