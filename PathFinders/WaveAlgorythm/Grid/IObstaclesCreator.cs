using DS.System;

namespace WaveAlgorythm
{
    interface IObstaclesCreator
    {
        IWeightedGraph<Location> Create();
    }
}