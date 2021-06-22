using System;

namespace Game.Extensions
{
    [Serializable]
    public struct PrefabSettings
    {
        /// <summary>
        /// Количество экземпляров, которое нужно создать при старте.
        /// </summary>
        public int count;

        /// <summary>
        /// Префаб, который будет использован.
        /// </summary>
        public LifetimeMonoBehaviour prefab;
    }
}