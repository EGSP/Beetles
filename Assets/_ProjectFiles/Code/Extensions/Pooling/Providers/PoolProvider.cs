using System;
using System.Collections.Generic;
using Egsp.Core;
using UnityEngine;

namespace Game.Extensions
{
    public class PoolProvider : MonoBehaviour, IPrefabProvider
    {
        [Header("Global")] 
        [SerializeField] private bool useGlobal;
        
        [Header("Spot")]
        [SerializeField] private List<PrefabSettings> spotPrefabs;

        [Tooltip("Опционально, если пул используется локально.")]
        [SerializeField] private string poolName;

        /// <summary>
        /// Корневой объект на сцене.
        /// </summary>
        private GameObject _spotParent;

        /// <summary>
        /// Все созданные префабы в своих слоях.
        /// </summary>
        private List<PrefabLayer> _spotLayers = new List<PrefabLayer>();

        public string PoolName => poolName;
        
        /// <summary>
        /// Имеются ли пользователи, использующие провайдер.
        /// </summary>
        public bool HasUsers { get; private set; } 
        
        /// <summary>
        /// Содержит ли пул в себе префабы.
        /// </summary>
        public bool HasSpot { get; private set; }
        
        protected virtual void Awake()
        {
            // Инициализация пользователей.
            SetupUsers();

            // Создание спотового пула.
            CreateSpotPool();
        }

        public virtual Option<T> GetInstance<T>() where T : LifetimeMonoBehaviour
        {
            for (var i = 0; i < _spotLayers.Count; i++)
            {
                var layer = _spotLayers[i];
                if (layer.Is<T>())
                {
                    return layer.GetInstance() as T;
                }
            }
            
            return Option<T>.None;
        }

        public virtual Option<T> GetInstance<T>(Func<T, bool> predicate) where T : LifetimeMonoBehaviour
        {
            for (var i = 0; i < _spotLayers.Count; i++)
            {
                var layer = _spotLayers[i];
                if (layer.Is<T>(predicate))
                {
                    return layer.GetInstance() as T;
                }
            }
            
            return Option<T>.None;
        }

        /// <summary>
        /// Возвращает слой для указанного типа экземпляра префаба.
        /// </summary>
        public Option<PrefabLayer> GetLayer<T>() where T : LifetimeMonoBehaviour
        {
            for (var i = 0; i < _spotLayers.Count; i++)
            {
                var layer = _spotLayers[i];
                if (layer.Is<T>())
                {
                    return layer;
                }
            }
            return Option<PrefabLayer>.None;
        }

        public Option<PrefabLayer> GetLayer<T>(Func<T, bool> predicate)
            where T : LifetimeMonoBehaviour
        {
            for (var i = 0; i < _spotLayers.Count; i++)
            {
                var layer = _spotLayers[i];
                if (layer.Is<T>(predicate))
                {
                    return layer;
                }
            }
            return Option<PrefabLayer>.None;
        }

        private void SetupUsers()
        {
            // Проверяем существование пользователей.
            var prefabUsers = GetUsers();
            if (prefabUsers.IsNone)
            {
                HasUsers = false;
            }
            else
            {
                HasUsers = true;
                
                // Устанавливаем провайдера пользователям.
                SetupUsersProvider(new NotNone<IPrefabUser[]>(prefabUsers));
            }
            
            Option<IPrefabUser[]> GetUsers()
            {
                // Проверяем существование пользователей.
                var users = GetComponents<IPrefabUser>();
                if (users.Length == 0)
                    return Option<IPrefabUser[]>.None;

                return users;
            }

            void SetupUsersProvider(NotNone<IPrefabUser[]> users)
            {
                var array = users.Instance;

                for (var i = 0; i < array.Length; i++)
                {
                    array[i].PrefabProvider = this;
                }
            }
        }

        private void CreateSpotPool()
        {
            HasSpot = false;

            // Cоздаем игровые объекты 
            CreateSpotPoolSpace();

            // Создаем экземпляры префабов.
            for (var i = 0; i < spotPrefabs.Count; i++)
            {
                var settings = spotPrefabs[i];
                CreateSpotLayer(settings.prefab, settings.count);
            }
            
            if (_spotLayers.Count == 0)
                return;

            HasSpot = true;
        }
        
        private void CreateSpotPoolSpace()
        {
            _spotParent = new GameObject($"[Pool Provider] {gameObject.name}");
        }

        private void CreateSpotLayer(LifetimeMonoBehaviour prefab, int count)
        {
            var layer = new PrefabLayer(prefab, InstantiatePrefab, _spotParent, count);
            _spotLayers.Add(layer);
        }

        // Функция для передачи PrefabLayer.
        private LifetimeMonoBehaviour InstantiatePrefab(LifetimeMonoBehaviour prefab)
        {
            return Instantiate(prefab);
        }
    }
}