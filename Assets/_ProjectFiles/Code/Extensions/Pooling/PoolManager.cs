using System.Collections.Generic;
using Egsp.Core;

namespace Game.Extensions
{
    [LazyInstance(false)]
    public class PoolManager : SingletonRaw<PoolManager>
    {
        private List<PoolProvider> _pools = new List<PoolProvider>();

        public Option<PoolProvider> GetPool(string name)
        {
            var coincidence = _pools.FirstOrNone(x
                => x.PoolName == name);

            return coincidence;
        }
    }
}