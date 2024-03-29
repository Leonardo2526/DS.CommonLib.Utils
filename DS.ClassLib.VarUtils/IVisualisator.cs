﻿using DS.ClassLib.VarUtils.Basis;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    public interface IVisualisator
    {
        public void Show();
    }

    /// <summary>
    /// The interface is used to show <typeparamref name="TItem"/>. 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface IItemVisualisator<TItem>
    {
        /// <summary>
        /// Show <paramref name="item"/>.
        /// </summary>
        /// <param name="item"></param>
        public void Show(TItem item);


        /// <summary>
        /// Show <paramref name="item"/> asyncronously.
        /// </summary>
        /// <param name="item"></param>
        public Task ShowAsync(TItem item);
    }

    /// <summary>
    /// The interface is used to show <typeparamref name="TItem"/>. 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public interface IItemResultVisualisator<TItem, TResult>
    {
        /// <summary>
        /// Show <paramref name="item"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// <typeparamref name="TResult"/> of visualization.
        /// </returns>
        public TResult Show(TItem item);

        /// <summary>
        /// Show <paramref name="item"/> asyncronously.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        ///  <typeparamref name="TResult"/> of visualization.
        /// </returns>
        public Task<TResult> ShowAsync(TItem item);
    }

    public interface IObjectVisualisator
    {
        public void Show(object objectToShow);
    }

    /// <summary>
    /// The interface is used to show <typeparamref name="T"/> poins. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPointVisualisator<T>
    {
        /// <summary>
        /// Show <typeparamref name="T"/> poins.
        /// </summary>
        /// <param name="point"></param>
        public void Show(T point);

        /// <summary>
        /// Show vector between <paramref name="point1"/> and <paramref name="point2"/>.   
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// </summary>
        public void ShowVector(T point1, T point2);

        /// <summary>
        /// Show vector from <paramref name="origin"/> by <paramref name="direction"/>.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        public void ShowVectorByDirection(T origin, T direction);

        /// <summary>
        /// Show basis.
        /// </summary>
        /// <param name="basis"></param>
        public void Show(Basis3d basis);
    }
}
