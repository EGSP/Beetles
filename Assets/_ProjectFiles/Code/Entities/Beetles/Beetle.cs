using UnityEngine;

namespace Game.Entities.Beetles
{
    /// <summary>
    /// Базовый класс для всех жуков.
    /// </summary>
    public class Beetle : MonoBehaviour, IPickable
    {
        public bool CanBePicked { get; private set; } = false;
        
        public void Picked()
        {
        }

        public void Put()
        {
        }
    }
}