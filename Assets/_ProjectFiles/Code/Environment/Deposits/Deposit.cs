using System.Collections;
using Egsp.Core;
using Game.Entities.Resources;
using Game.Extensions;
using UnityEngine;

namespace Game.Environment.Deposits
{
    public class Deposit : MonoBehaviour, IPrefabUser
    {
        [SerializeField] private ResourceRarity resourceRarity;
        
        [SerializeField] private float spawnTime;
        [SerializeField] private Transform spawnPlace;
        
        private IEnumerator _spawnResourceRoutine;
        
        private Option<Resource> _resource  = Option<Resource>.None; 
        
        public IPrefabProvider PrefabProvider { get; set; }

        private void Awake()
        {
            Spawn();
        }

        public Option<Resource> PickResource()
        {
            var resource = _resource;
            _resource = Option<Resource>.None;
            
            if(_resource.IsNone)
                Spawn();
            
            return resource;
        }

        private void Spawn()
        {
            if (_spawnResourceRoutine != null)
                return;

            _spawnResourceRoutine = SpawnResource();
            StartCoroutine(_spawnResourceRoutine);
        }

        private IEnumerator SpawnResource()
        {
            Debug.Log("Spawn start.");
            if (_resource.IsSome)
            {
                Debug.Log("Resource already spawned.");
                _spawnResourceRoutine = null;
                yield break;
            }
            
            yield return new WaitForSeconds(spawnTime);

            Debug.Log("Get resource from provider.");
            
            var resource = PrefabProvider.GetInstance<Resource>(
                x => x.Rarity == resourceRarity);

            if (resource.IsNone)
            {
                Debug.Log($"{gameObject.name} Депозит не смог заспавнить ресурс. Не найдено подходящего префаба.");
            }
            else
            {
                _resource = resource;
                _resource.Value.transform.position = spawnPlace.position;
                _spawnResourceRoutine = null;
            }
        }
    }
}