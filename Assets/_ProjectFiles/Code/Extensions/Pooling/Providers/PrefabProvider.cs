using System;
using System.Collections.Generic;
using Egsp.Core;
using UnityEngine;

namespace Game.Extensions
{
    /// <summary>
    /// Дополняет функционал пул провайдера тем что ищет другие пулы.
    /// </summary>
    public class PrefabProvider : PoolProvider
    {
        [Header("Pool")] [Tooltip("Название внешнего пула.")]
        [SerializeField] private List<string> poolNames;

        /// <summary>
        /// Внешние используемые пулы.
        /// </summary>
        private List<PoolProvider> _pools  = new List<PoolProvider>();
        
        /// <summary>
        /// Хранит в себе связи между типом объекта и слоем префабов.
        /// </summary>
        private Dictionary<Type, PrefabLayer> _typeToLayer = new Dictionary<Type, PrefabLayer>();
        
        protected override void Awake()
        {
            base.Awake();
            
            SetupPools();
        }

        public override Option<T> GetInstance<T>()
        {
            var type = typeof(T);
            
            // Имеет собственные префабы.
            if (HasSpot)
            {
                var layer = IsCachedLayer<T>();
                // Если не найдено слоя для типа в кеше.
                if (layer.IsNone)
                {
                    var spotLayerO = GetLayer<T>();
                    // Если есть слой с подходящим типом объектов.
                    if (spotLayerO.IsSome)
                    {
                        var spotLayer = spotLayerO.Value;
                        CacheLayer(type, spotLayer);

                        var inst = spotLayer.GetInstance() as T;
                        return inst;
                    }
                    // Будем искать во внешних пулах.
                }
                // Найден слой в кеше.
                else
                {
                    var inst = layer.Value.GetInstance() as T;
                    return inst;
                }
            }
            // Если нет собственных префабов или не найдено префаба с нужным типом.
            else
            {
                for (var i = 0; i < _pools.Count; i++)
                {
                    var layerO = _pools[i].GetLayer<T>();
                    // В пуле нет подходящего слоя.
                    if(layerO.IsNone)
                        continue;

                    // Слой найден и может быть кеширован.
                    var layer = layerO.Value;
                    CacheLayer(type, layer);

                    return layer.GetInstance() as T;
                }
            }
            
            Debug.Log($"Не найдено ни одного пула для типа {type.Name} " +
                      $"пользователя {gameObject.name}");
            // Не найдено ни одного пула.
            return Option<T>.None;
        }

        public override Option<T> GetInstance<T>(Func<T, bool> predicate)
        {
            var type = typeof(T);
            
            // Имеет собственные префабы.
            if (HasSpot)
            {
                var layer = IsCachedLayer<T>();
                // Если не найдено слоя для типа в кеше.
                if (layer.IsNone)
                {
                    var spotLayerO = GetLayer<T>(predicate);
                    // Если есть слой с подходящим типом объектов.
                    if (spotLayerO.IsSome)
                    {
                        var spotLayer = spotLayerO.Value;
                        CacheLayer(type, spotLayer);

                        var inst = spotLayer.GetInstance() as T;
                        return inst;
                    }
                    // Будем искать во внешних пулах.
                }
                // Найден слой в кеше.
                else
                {
                    var inst = layer.Value.GetInstance() as T;
                    return inst;
                }
            }
            // Если нет собственных префабов или не найдено префаба с нужным типом.
            else
            {
                for (var i = 0; i < _pools.Count; i++)
                {
                    var layerO = _pools[i].GetLayer<T>(predicate);
                    // В пуле нет подходящего слоя.
                    if(layerO.IsNone)
                        continue;

                    // Слой найден и может быть кеширован.
                    var layer = layerO.Value;
                    CacheLayer(type, layer);

                    return layer.GetInstance() as T;
                }
            }
            
            Debug.Log($"Не найдено ни одного пула для типа {type.Name} " +
                      $"пользователя {gameObject.name}");
            // Не найдено ни одного пула.
            return Option<T>.None;
        }

        /// <summary>
        /// Кеширован ли запрос к объектам данного типа.
        /// </summary>
        /// <returns></returns>
        private Option<PrefabLayer> IsCachedLayer<T>()
        {
            var type = typeof(T);

            PrefabLayer value;
            if (_typeToLayer.TryGetValue(type, out value))
            {
                return value;
            }
            
            return Option<PrefabLayer>.None;
        }

        private void CacheLayer(Type type, PrefabLayer layer)
        {
            _typeToLayer.Add(type, layer);
        }
        
        private void SetupPools()
        {
            _pools.Capacity = poolNames.Count;
            
            for (var i = 0; i < poolNames.Count; i++)
            {
                var pool = PoolManager.Instance.GetPool(poolNames[i]);

                if (pool.IsNone)
                {
                    Debug.Log($"Не найдено пула с названием {poolNames[i]}");
                    return;
                }
                else
                {
                    // Если подобного пула нет в списке
                    if (!_pools.Contains(pool.Value))
                    {
                        _pools.Add(pool.Value);
                    }
                }
            }
        }
    }
}