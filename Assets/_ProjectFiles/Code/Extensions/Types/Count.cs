using System;
using Egsp.Core;

namespace Game.Extensions
{
    /// <summary>
    /// Структура хранит в себе количество некоторого объекта.
    /// </summary>
    public readonly struct Count<T>
    {
        /// <summary>
        /// Подсчитываемый объект. 
        /// </summary>
        public readonly NotNone<T> Object;

        /// <summary>
        /// Количество объекта.
        /// </summary>
        public readonly int count;

        public Count(NotNone<T> o, int count)
        {
            Object = o;
            this.count = count;
        }

        public static implicit operator T(Count<T> count)
        {
            return count.Object;
        }
    }
}