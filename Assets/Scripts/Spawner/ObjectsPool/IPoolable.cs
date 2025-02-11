using System;
using UnityEngine;

namespace Spawner.ObjectsPool
{
    public interface IPoolable
    {
        GameObject GameObject { get; }
        event Action<IPoolable> Destroyed;
        void Reset();
    }
}