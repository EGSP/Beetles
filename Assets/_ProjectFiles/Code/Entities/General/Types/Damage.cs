using System;

namespace Game.Entities
{
    /// <summary>
    /// Значение урона.
    /// </summary>
    [Serializable]
    public readonly struct Damage
    {
        public readonly uint Value;

        public Damage(uint value)
        {
            Value = value;
        }
        
        public static implicit operator uint(Damage damage)
            => damage.Value;

        public static explicit operator Damage(uint damage)
            => new Damage(damage);
    }
}