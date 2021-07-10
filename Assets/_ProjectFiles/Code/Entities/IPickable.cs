using Game.Extensions;

namespace Game.Entities
{
    /// <summary>
    /// Базовый интерфейс для сущностей, которые могут быть подобраны.
    /// </summary>
    public interface IPickable : ILifetime
    {
        /// <summary>
        /// Может ли сущность быть подобрана
        /// </summary>
        bool CanBePicked { get; }

        /// <summary>
        /// Сущность была подобрана.
        /// </summary>
        void PickIt();
        
        /// <summary>
        /// Сущность положили.
        /// </summary>
        void PutIt();
    }
}