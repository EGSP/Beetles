using System.Collections.Generic;
using Egsp.Core;
using UnityEngine;

namespace Game.Extensions
{
    [LazyInstance(false)]
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField] private List<PoolProvider> pools;

        public Option<PoolProvider> GetPool(string poolName)
        {
            var coincidence = pools.FirstOrNone(x
                => x.PoolName == poolName);

            return coincidence;
        }
    }
}