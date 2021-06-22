namespace Game.Extensions
{
    /// <summary>
    /// Способ, которым будет завершен жизненный цикл объекта.
    /// </summary>
    public enum TerminateFormat
    {
        /// <summary>
        /// Объект будет выключен.
        /// </summary>
        Deactivate,
        /// <summary>
        /// Объект будет уничтожен.
        /// </summary>
        Destroy
    }
}