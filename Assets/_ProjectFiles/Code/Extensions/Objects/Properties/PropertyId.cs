using System;

namespace Game.Extensions
{
    public readonly struct PropertyId
    {
        public readonly Type Value;

        public PropertyId(Type value)
        {
            Value = value;
        }
        
        public static implicit operator PropertyId(Type type)
        {
            return new PropertyId(type);
        }
    }
}