using System.Collections;
using Game.Entities.Resources;
using UnityEngine;

namespace Game.Environment.Deposits
{
    public class Deposit : MonoBehaviour
    {
        [SerializeField] private ResourceRarity resourceRarity;
        
        [SerializeField] private float spawnTime;
        [SerializeField] private Transform spawnPlace;
        
        private IEnumerator spawnTimerRoutine;
    }
}