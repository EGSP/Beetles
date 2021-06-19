namespace Game.Entities.Resources
{
    /// <summary>
    /// Класс представляющий ресурс вне пространства игровых объектов.
    /// </summary>
    public class ResourceRaw
    {
        public readonly ResourceRarity Rarity;

        public ResourceRaw(ResourceRarity rarity)
        {
            Rarity = rarity;
        }
    }
}