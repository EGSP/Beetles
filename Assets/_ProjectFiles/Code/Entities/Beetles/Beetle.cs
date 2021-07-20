using System;
using Game.Extensions;
using JetBrains.Lifetimes;
using UnityEngine;

namespace Game.Entities.Beetles
{
    /// <summary>
    /// Базовый класс для всех жуков.
    /// </summary>
    public partial class Beetle : LifetimeMonoBehaviour, IPickable
    {
        [Header("Health")]
        [SerializeField] private Health health;
        [Tooltip("Время, которое существует жук после потери всего здоровья")]
        [Range(0,360)]
        [SerializeField] private float knockoutTime;

        [Header("Body")] 
        [SerializeField] private BeetleBody body;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform pickablePlace;
        [SerializeField] private Rigidbody rigidBody;
        
        [Header("Attack")]
        [SerializeField] private Damage damage;
        
        /// <summary>
        /// Подобранный жуком объект.
        /// </summary>
        private Linked<IPickable> _pickedObject;

        public bool CanBePicked { get; private set; } = false;

        public BeetleBody Body => body;

        private void Awake()
        {
            _pickedObject = new Linked<IPickable>(Lifetime.Terminated, null);
            SetupPhysicsBody();
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
        }

        private void LateUpdate()
        {
            CheckPhysics();
        }

        public void PickIt()
        {
        }

        public void PutIt()
        {
        }

        private PickResult Pick(IPickable pickable)
        {
            if (_pickedObject.Lifetime.IsAlive)
                return PickResult.NotEnoughSpace;
            
            if (pickable.IsTerminated)
                return PickResult.PickableTerminated;

            if (!pickable.CanBePicked)
                return PickResult.PickableCannotBePicked;
            
            pickable.PickIt();
            
            if (pickable is MonoBehaviour mono)
            {
                var monoTransform = mono.transform;
                monoTransform.SetParent(pickablePlace);
                monoTransform.localPosition = Vector3.zero;
            }

            _pickedObject = new Linked<IPickable>(pickable.Lifetime, pickable);
            _pickedObject.OnTermination(OnPickableTerminated);
            
            return PickResult.Picked;
        }

        /// <summary>
        /// Кладем переносимый объект на земплю.
        /// </summary>
        private void Put()
        {
            if (_pickedObject.Lifetime.IsAlive)
            {
                var mono = (MonoBehaviour) _pickedObject.Value;
                var monoTransform = mono.transform;
                
                monoTransform.SetParent(null);
                monoTransform.position = transform.position;
                
                _pickedObject.Value.PutIt();
                _pickedObject.Release();
            }
            else
            {
                Debug.Log("Нечего класть.");
            }
        }

        private void OnPickableTerminated(IPickable pickable)
        {
            if (_pickedObject.Equals(pickable))
            {
                Debug.Log("Объект был уничтожен на жуке.");
            }
            else
            {
                Debug.Log("Прошлый подобранный объект хранил ссылку на жука!");
            }
        }
        
        private enum PickResult
        {
            Picked,
            /// <summary>
            /// Подбираемый объект уничтожен.
            /// </summary>
            PickableTerminated,
            /// <summary>
            /// Подбираемый объект не может быть подобран.
            /// </summary>
            PickableCannotBePicked,
            /// <summary>
            /// Некуда было взять объект.
            /// </summary>
            NotEnoughSpace
        }
    }
}