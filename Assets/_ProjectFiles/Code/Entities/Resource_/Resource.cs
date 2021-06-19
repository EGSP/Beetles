using UnityEngine;

namespace Game.Entities.Resources
{
    /// <summary>
    /// Базовый класс для всех ресурсов.
    /// </summary>
    public class Resource : MonoBehaviour, IPickable
    {
        [SerializeField] private ResourceRarity _rarity;

        public ResourceRarity Rarity => _rarity;

        public bool CanBePicked { get; private set; } = true;
        
        public void Picked()
        {
        }

        public void Put()
        {
        }
    }
}