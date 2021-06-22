using Egsp.Core;
using JetBrains.Lifetimes;

namespace Game.Extensions
{
    public interface ILifetime
    {
        Lifetime Lifetime { get; }
        
        /// <summary>
        /// Способ, которым будет завершен жизненный цикл объекта.
        /// </summary>
        TerminateFormat Format { get; }
    }
}