using UnityEngine;

namespace Game.Entities.Beetles
{
    [System.Serializable]
    public class BeetleBody
    {
        [SerializeField] private GameObject head;
        [SerializeField] private GameObject back;

        public GameObject Head => head;
        public GameObject Back => back;
    }
}