namespace Game
{
    /// <summary>
    /// Представляет название команды.
    /// </summary>
    public readonly struct TeamName
    {
        public readonly string Value;

        public TeamName(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(TeamName name) => name.Value;
    }
}