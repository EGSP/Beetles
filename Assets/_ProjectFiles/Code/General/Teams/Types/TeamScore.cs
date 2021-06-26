namespace Game
{
    /// <summary>
    /// Представляет количество очков определенной команды.
    /// </summary>
    public readonly struct TeamScore
    {
        /// <summary>
        /// Счет команды.
        /// </summary>
        public readonly uint Value;

        public TeamScore(uint value)
        {
            Value = value;
        }

        public static explicit operator TeamScore(uint value)
            => new TeamScore(value);

        public static implicit operator uint(TeamScore score)
            => score.Value;

        public static TeamScore operator +(TeamScore score, uint value) 
            => new TeamScore(score.Value + value);

        public static TeamScore operator -(TeamScore score, uint value)
        {
            if (value >= score.Value)
                return new TeamScore(0);

            return new TeamScore(score.Value - value);
        }
    }
}