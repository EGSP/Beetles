using System;
using System.Collections.Generic;
using Egsp.Core;
using UnityEngine;

namespace Game.Extensions
{
    public class SpotProvider : MonoBehaviour, IPrefabProvider
    {
        [SerializeField] private List<PrefabSettings> prefabs;

        private GameObject _poolParent;

        private List<PrefabLayer> _layers = new List<PrefabLayer>();
        
        private void Awake()
        {
            // Проверяем существование пользователей.
            var prefabUsers = GetComponents<IPrefabUser>();
            if (prefabUsers.Length == 0)
                return;
            
            // Устанавливаем провайдера пользователям.
            for (var i = 0; i < prefabUsers.Length; i++)
            {
                prefabUsers[i].PrefabProvider = this;
            }
            
            CreatePoolSpace();

            // Создаем экземпляры префабов.
            for (var i = 0; i < prefabs.Count; i++)
            {
                var settings = prefabs[i];
                CreatePrefabLayers(settings.prefab, settings.count);
            }
        }
        
        private void CreatePoolSpace()
        {
            _poolParent = new GameObject($"[Pool Provider] {gameObject.name}");
        }

        private void CreatePrefabLayers(LifetimeMonoBehaviour prefab, int count)
        {
            var layer = new PrefabLayer(prefab, InstantiatePrefab, _poolParent, count);
            _layers.Add(layer);
        }

        private LifetimeMonoBehaviour InstantiatePrefab(LifetimeMonoBehaviour prefab)
        {
            return Instantiate(prefab);
        }

        public Option<T> GetPrefab<T>() where T : LifetimeMonoBehaviour
        {
            for (var i = 0; i < _layers.Count; i++)
            {
                var layer = _layers[i];
                if (layer.Is<T>())
                {
                    return layer.GetInstance() as T;
                }
            }
            
            return Option<T>.None;
        }
        
        public Option<T> GetPrefab<T>(Func<T, bool> predicate) where T : LifetimeMonoBehaviour
        {
            for (var i = 0; i < _layers.Count; i++)
            {
                var layer = _layers[i];
                if (layer.Is<T>(predicate))
                {
                    return layer.GetInstance() as T;
                }
            }
            
            return Option<T>.None;
        }
    }
}