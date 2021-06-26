namespace Game
{
    /// <summary>
    /// Представляет идентификатор команды.
    /// </summary>
    public readonly struct TeamId
    {
        public readonly uint Value;

        public TeamId(uint value)
        {
            Value = value;
        }

        public static implicit operator uint(TeamId id) => id.Value;
    }
}