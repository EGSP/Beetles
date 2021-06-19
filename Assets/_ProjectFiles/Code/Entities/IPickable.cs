namespace Game.Entities
{
    /// <summary>
    /// Базовый интерфейс для сущностей, которые могут быть подобраны.
    /// </summary>
    public interface IPickable
    {
        /// <summary>
        /// Может ли сущность быть подобрана
        /// </summary>
        bool CanBePicked { get; }

        /// <summary>
        /// Сущность была подобрана.
        /// </summary>
        void Picked();
        
        /// <summary>
        /// Сущность положили.
        /// </summary>
        void Put();
    }
}