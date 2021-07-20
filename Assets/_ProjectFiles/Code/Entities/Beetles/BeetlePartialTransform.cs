using UnityEngine;

namespace Game.Entities.Beetles
{
    public partial class Beetle
    {
        public void Move(float forward, float right)
        {
            var velocity = transform.right * right + transform.forward * forward;
            velocity.Normalize();
            MoveVelocity = velocity * moveSpeed;
        }

        public void RotateY(float angle)
        {
            rigidBody.rotation = rigidBody.rotation * Quaternion.Euler(0, angle, 0);
        }
    }
}