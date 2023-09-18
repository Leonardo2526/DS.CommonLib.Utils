using Castle.Core.Internal;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Cartesian coordinate system built by 
    /// <see cref="Vector3d.XAxis"/>, <see cref="Vector3d.YAxis"/> and <see cref="Vector3d.ZAxis"/> 
    /// with <see cref="Point3d.Origin"/> as origin point. 
    /// <para>
    /// It can change activity of it's own objects.
    /// </para>
    /// </summary>
    public class CCSModel : IDirectionValidator
    {
        private static readonly int _cTolerance = 3;
        private static readonly double _cT = Math.Pow(0.1, _cTolerance);
        private readonly bool _defaultActivity = true;
        private readonly (Rectangle3d Rectangle, bool IsEnable)[] _quadrants =
            new (Rectangle3d, bool)[12];
        private readonly IOctantModel[] _octantsModel = new IOctantModel[8];

        /// <summary>
        /// Instansiate a cartesian coordinate system built by 
        /// <see cref="Vector3d.XAxis"/>, <see cref="Vector3d.YAxis"/> and <see cref="Vector3d.ZAxis"/> 
        /// with <see cref="Point3d.Origin"/> as origin point. 
        /// <para>
        /// It can change activity of it's own objects.
        /// </para>
        /// </summary>
        public CCSModel()
        {
            for (int i = 0; i < CCS.Octants.Length; i++)
            {
                var o = CCS.Octants[i];
                _octantsModel[i] = new OctantModel(o.Basis, o.Quadrants, o.Box, _defaultActivity);
            }

            for (int i = 0; i < CCS.Quadrants.Length; i++)
            {
                _quadrants[i] = (CCS.Quadrants[i], _defaultActivity);
            }

        }

        #region Properties

        /// <summary>
        /// Eight octants that represent boxes divided by half-axes.
        /// </summary>
        public IOctantModel[] OctantsModel => _octantsModel;

        /// <summary>
        /// The octant's plane bounded by <see cref="Basis.Basis3d.X"/> and <see cref="Basis.Basis3d.Y"/>.
        /// </summary>
        public (Rectangle3d Rectangle, bool IsEnable)[] XYQuadrantsModel =>
            GetQuadrantsModel(Vector3d.ZAxis);

        /// <summary>
        /// The octant's plane bounded by <see cref="Basis.Basis3d.X"/> and <see cref="Basis.Basis3d.Z"/>.
        /// </summary>
        public (Rectangle3d Rectangle, bool IsEnable)[] XZQuadrantsModel =>
            GetQuadrantsModel(Vector3d.YAxis);

        /// <summary>
        /// The octant's plane bounded by <see cref="Basis.Basis3d.Y"/> and <see cref="Basis.Basis3d.Z"/>.
        /// </summary>
        public (Rectangle3d Rectangle, bool IsEnable)[] YZQuadrantsModel =>
          GetQuadrantsModel(Vector3d.XAxis);

        /// <summary>
        /// Twelfe infinite regions that represent planes divided by half-axes.
        /// </summary>
        public (Rectangle3d Rectangle, bool IsEnable)[] QuadrantsModel
        => GetAllQuadrantsModel();

        #endregion


        #region PublicMethods

        /// <summary>
        /// Change <see cref="IOctantModel"/> <paramref name="activity"/> 
        /// by <paramref name="octantId"/>.
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="octantId"></param>
        public void SetOctantActivity(bool activity, int octantId)
        {
            OctantsModel[octantId].IsEnable = activity;
        }

        /// <summary>
        /// Change <see cref="IOctantModel.BoxModel"/> <paramref name="activity"/> 
        /// by <paramref name="octantId"/>.
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="octantId"></param>
        public void SetBoxActivity(int octantId, bool activity)
        {
            OctantsModel[octantId].SetBoxActivity(activity);
        }

        /// <summary>
        /// Change <see cref="IOctantModel.QuadrantsModel"/> <paramref name="activity"/> 
        /// by <paramref name="octantId"/>.
        /// <para>
        /// Only quadrants at <paramref name="orthoPlane"/> change activity.
        /// If <paramref name="orthoPlane"/> has default value, all quadrants activity will be changed.
        /// </para>      
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="octantId"></param>
        /// <param name="orthoPlane"></param>
        public void SetQuadrantActivity(int octantId, OrthoPlane orthoPlane, bool activity)
        {
            OctantsModel[octantId].SetQuadrantsActivity(activity, orthoPlane);
        }

        /// <summary>
        /// Change quadrants <paramref name="activity"/>.
        /// <para>
        /// Only quadrants at <paramref name="orthoPlane"/> and that
        /// contains <paramref name="axis"/> change activity.
        /// <para>
        /// If <paramref name="orthoPlane"/> has default value, 
        /// all quadrants activity by <paramref name="axis"/> will be changed.
        /// </para>
        /// </para>      
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="axis"></param>
        /// <param name="orthoPlane"></param>
        public void SetQuadrantsActivity(bool activity, Vector3d axis, OrthoPlane orthoPlane = 0)
        {
            OctantsModel.ForEach(m => m.SetQuadrantsActivity(activity, axis, orthoPlane));
        }

        /// <summary>
        /// Change quadrants <paramref name="activity"/>.
        /// <para>
        /// Only quadrants at <paramref name="orthoPlane"/> change activity.
        /// If <paramref name="orthoPlane"/> has default value, all quadrants activity will be changed.
        /// </para>      
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="orthoPlane"></param>
        public void SetQuadrantsActivity(bool activity, OrthoPlane orthoPlane = 0)
        {
            OctantsModel.ForEach(m => m.SetQuadrantsActivity(activity, orthoPlane));
        }

        /// <inheritdoc/>
        public bool IsValid(Point3d point)
        {
            return OctantsModel.Any(m => m.IsValid(point));
        }

        /// <inheritdoc/>
        public bool IsValid(Vector3d vector)
        {
            return OctantsModel.Any(m => m.IsValid(vector));
        }

        #endregion

        private (Rectangle3d Rectangle, bool IsEnable)[] GetQuadrantsModel(Vector3d normal)
        {
            int ind;
            if (normal == Vector3d.ZAxis) { ind = 0; }
            else if (normal == Vector3d.YAxis) { ind = 1; }
            else { ind = 2; }
            var quadrants = OctantsModel.Select(o => o.QuadrantsModel[ind]).
                Where(q => q.Rectangle.Plane.Normal.IsParallelTo(normal) == 1);
            return quadrants.ToArray();

            //var quadrants = new List<(Rectangle3d, bool)>();
            //foreach (var om in OctantsModel)
            //{
            //    foreach (var qm in om.QuadrantsModel)
            //    {
            //        if (qm.Rectangle.Plane.Normal.IsParallelTo(normal) == 1)
            //        { quadrants.Add(qm); }
            //    }
            //}

        }

        private (Rectangle3d Rectangle, bool IsEnable)[] GetAllQuadrantsModel()
        {
            var quadrants = new List<(Rectangle3d, bool)>();
            quadrants.AddRange(XYQuadrantsModel);
            quadrants.AddRange(XZQuadrantsModel);
            quadrants.AddRange(YZQuadrantsModel);
            return quadrants.ToArray();
        }
    }
}
