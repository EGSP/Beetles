using System;

namespace Egsp.Core
{
    /// <summary>
    /// Структура допускает использование только существующего значения вне зависимости от типа объекта.
    /// </summary>
    public readonly struct NotNone<TValue>
    {
        /// <summary>
        /// Если данное значение null, значит кто-то использовал пустой конструктор.
        /// </summary>
        public readonly TValue Instance;

        public NotNone(TValue value)
        {
            Instance = value ?? throw new NullReferenceException();
        }

        public NotNone(Option<TValue> option)
        {
            Instance = option.IsSome ? option.Value : throw new NoneValueException();
        }

        public static implicit operator TValue(NotNone<TValue> value)
        {
            return value.Instance;
        }
        
        public static implicit operator NotNone<TValue>(TValue value)
        {
            return new NotNone<TValue>(value);
        }
    }
}