using Egsp.Core;
using JetBrains.Lifetimes;

namespace Game.Extensions
{
    // Некоторые моменты будут повторять функционал Lifetime, однако они нужны в собственной реализации,
    // т.к. логика отличается от стандартных объектов.
    
    public interface ILifetime
    {
        Lifetime Lifetime { get; }
        
        /// <summary>
        /// Способ, которым будет завершен жизненный цикл объекта.
        /// </summary>
        TerminateFormat Format { get; }
        
        /// <summary>
        /// Терминирован ли объект.
        /// </summary>
        bool IsTerminated { get; }

        void Parent(Lifetime parentLifetime);

        void ChangeTerminateFormat(TerminateFormat format);
    }
}