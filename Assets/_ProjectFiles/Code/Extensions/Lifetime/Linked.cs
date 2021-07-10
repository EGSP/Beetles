using System;
using JetBrains.Lifetimes;

namespace Game.Extensions
{
    public class Linked<T> : ITerminationHandler
    {
        private readonly LifetimeDefinition _lifetimeDefinition;
        
        /// <summary>
        /// Действие, выполняемое при терминации.
        /// </summary>
        private Action<T> _terminationAction;

        public Lifetime Lifetime { get; }
        
        public T Value { get; private set; }
        
        public Linked(Lifetime targetLifetime, T value)
        {
            _lifetimeDefinition = targetLifetime.CreateNested();
            Lifetime = _lifetimeDefinition.Lifetime;
            Lifetime.OnTermination(this);
            
            Value = value;
        }

        public Linked(Lifetime targetLifetime, T value, Action<T> terminationAction)
            : this(targetLifetime,value)
        {
            _terminationAction = terminationAction;
        }
        
        void ITerminationHandler.OnTermination(Lifetime lifetime)
        {
            _terminationAction?.Invoke(Value);
            Release();
        }

        /// <summary>
        /// Освобождает связь. Убирает ссылки на объект делегата.
        /// </summary>
        public void Release()
        {
            Value = default(T);
            _terminationAction = null;
            
            if (Lifetime.IsAlive)
                _lifetimeDefinition.Terminate();
        }
        
        /// <summary>
        /// Устанавливает действие при уничтожении связи.
        /// </summary>
        public void OnTermination(Action<T> action) => _terminationAction = action;

        /// <summary>
        /// Равно ли значение значению данного экземпляра.
        /// </summary>
        public bool Equals(T value)
        {
            if (Value == null || value == null)
                return false;

            return Value.Equals(value);
        }
    }
}