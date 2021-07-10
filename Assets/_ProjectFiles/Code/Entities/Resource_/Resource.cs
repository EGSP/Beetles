using System;
using System.Collections;
using Game.Extensions;
using UnityEngine;

namespace Game.Entities.Resources
{
    /// <summary>
    /// Базовый класс для всех ресурсов.
    /// </summary>
    [TerminateFormat(TerminateFormat.Deactivate)]
    public class Resource : LifetimeMonoBehaviour, IPickable
    {
        [SerializeField] private ResourceRarity _rarity;
        [Tooltip("Время, по истечению которого, ресурс будет уничтожен, если он лежит.")]
        [SerializeField] private float terminationTime;

        private bool _terminate;
        private float _timeToTermination;
        
        public ResourceRarity Rarity => _rarity;

        public bool CanBePicked { get; private set; } = true;

        private void Update()
        {
            if (_terminate)
            {
                _timeToTermination -= Time.deltaTime;

                if (_timeToTermination < 0)
                {
                    Terminate();
                }
            }
        }

        public void PickIt()
        {
            StopTermination();
            Lock();
        }

        public void PutIt()
        {
            StartTermination();
            Unlock();
        }
        
        /// <summary>
        /// Заблокировать возможность поднятия.
        /// </summary>
        public void Lock()
        {
            CanBePicked = false;
        }

        /// <summary>
        /// Разблокировать возможность поднятия.
        /// </summary>
        public void Unlock()
        {
            CanBePicked = true;
        }

        private void StartTermination()
        {
            _terminate = true;
            _timeToTermination = terminationTime;
        }

        private void StopTermination()
        {
            _terminate = false;
        }
    }
}