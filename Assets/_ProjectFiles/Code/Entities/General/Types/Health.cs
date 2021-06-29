namespace Game.Entities
{
    /// <summary>
    /// Значение здоровья сущности.
    /// </summary>
    public readonly struct Health
    {
        public readonly uint Value;

        public Health(uint value)
        {
            Value = value;
        }

        public static implicit operator uint(Health health)
            => health.Value;

        public static explicit operator Health(uint health)
            => new Health(health);
    }
}