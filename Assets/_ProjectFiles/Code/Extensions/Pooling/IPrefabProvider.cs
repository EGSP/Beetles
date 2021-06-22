using System;
using Egsp.Core;

namespace Game.Extensions
{
    /// <summary>
    /// Предоставляет доступ к получению префабов.
    /// </summary>
    public interface IPrefabProvider
    {
        /// <summary>
        /// Возвращает префаб заданного типа.
        /// </summary>
        Option<T> GetPrefab<T>() where T : LifetimeMonoBehaviour;

        Option<T> GetPrefab<T>(Func<T,bool> predicate) where T : LifetimeMonoBehaviour;
    }
}