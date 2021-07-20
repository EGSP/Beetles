using UnityEngine;

namespace Game.Entities.Beetles
{
    public partial class Beetle
    {
        private Collider _collider = null;
        private bool _usePhysics;
        
        /// <summary>
        /// Скорость/направление движения.
        /// </summary>
        private Vector3 _moveVelocity;

        private Vector3 _previousVelocity = Vector3.zero;

        public Vector3 MoveVelocity
        {
            get => _moveVelocity;
            set => _moveVelocity = value;
        }

        private bool UsePhysics
        {
            get => _usePhysics;
            set
            {
                if (rigidBody == null)
                    return;

                rigidBody.isKinematic = !value;
                _usePhysics = value;
            }
        }

        private void UpdateVelocity()
        {
            if (!UsePhysics)
                return;
            
            var velocity = rigidBody.velocity;
            var velocityChangeVector = _moveVelocity;

            for (var i = 0; i < 3; i++)
            {
                velocityChangeVector[i] = _moveVelocity[i] - velocity[i];
            }
            
            velocityChangeVector.y = 0;

            rigidBody.AddForce(velocityChangeVector, ForceMode.VelocityChange);
            _previousVelocity = _moveVelocity;
        }
        
        private void SetupPhysicsBody()
        {
            if (rigidBody == null)
                rigidBody = GetComponentInChildren<Rigidbody>();

            if (_collider == null)
                _collider = GetComponentInChildren<Collider>();
        }

        private void CheckPhysics()
        {
            var colliderIsWorking = CheckCollider();

            // Если физика отключена, то нужно проверить все условия для ее включения.
            if (!UsePhysics)
            {
                if (colliderIsWorking)
                    UsePhysics = true;
            }
        }

        private bool CheckCollider()
        {
            // Если нет коллайдера, то нужно отключить физику.
            if (_collider == null || !_collider.enabled)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}