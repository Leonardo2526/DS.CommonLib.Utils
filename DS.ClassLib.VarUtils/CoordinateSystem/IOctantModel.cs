using DS.ClassLib.VarUtils.Basis;
using Rhino.Geometry;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// An object that represents <see cref="Octant"/> that can change activity of it's own objects.
    /// </summary>
    public interface IOctantModel : IDirectionValidator
    {
        /// <summary>
        /// Specifies if <see cref="Octant"/> is enabled.
        /// </summary>
        bool IsEnable { get; set; }

        /// <summary>
        /// Specifies activity of each one axis of <see cref="Basis3d"/>.
        /// </summary>
        (Vector3d Axis, bool IsEnable)[] BasisModel { get; }

        /// <summary>
        /// Specifies activity of <see cref="BoundingBox"/>.
        /// </summary>
        (BoundingBox Box, bool IsEnable) BoxModel { get; }

        /// <summary>
        /// Specifies activity of quadrants.
        /// </summary>
        (Rectangle3d Rectangle, bool IsEnable)[] QuadrantsModel { get; }

        /// <summary>
        /// Change <see cref="BasisModel"/> <paramref name="activity"/>.
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="axis"></param>
        void SetBasisActivity(bool activity, Vector3d axis = default);

        /// <summary>
        /// Change <see cref="BoxModel"/> <paramref name="activity"/>.
        /// </summary>
        /// <param name="activity"></param>
        void SetBoxActivity(bool activity);

        /// <summary>
        /// Change quadrants <paramref name="activity"/>.
        /// <para>
        /// Only quadrants at <paramref name="orthoPlane"/> change activity.
        /// If <paramref name="orthoPlane"/> has default value, all quadrants activity will be changed.
        /// </para>      
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="orthoPlane"></param>
        void SetQuadrantsActivity(bool activity, OrthoPlane orthoPlane = 0);

        /// <summary>
        /// Change quadrants <paramref name="activity"/>.
        /// <para>
        /// Only quadrants that contains <paramref name="axis"/> change activity.      
        /// </para>      
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="axis"></param>
        void SetQuadrantsActivity(bool activity, Vector3d axis);

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
        void SetQuadrantsActivity(bool activity, Vector3d axis, OrthoPlane orthoPlane = 0);
    }
}