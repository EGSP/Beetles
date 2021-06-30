﻿using System;
using Egsp.Core;

namespace Game.Extensions
{
    /// <summary>
    /// Предоставляет доступ к получению префабов.
    /// </summary>
    public interface IPrefabProvider
    {
        /// <summary>
        /// Возвращает экземпляр заданного типа.
        /// </summary>
        Option<T> GetInstance<T>(Func<T, bool> predicate = null) where T : LifetimeMonoBehaviour;
    }
}