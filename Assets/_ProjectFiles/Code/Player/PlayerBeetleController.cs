using Game.Entities.Beetles;
using Game.Io;
using UnityEngine;

namespace Game.Player
{
    public class PlayerBeetleController : MonoBehaviour, IInputUser
    {
        private Beetle _beetle;

        private void Start()
        {
            _beetle = FindObjectOfType<Beetle>();
            if (_beetle == null)
                Debug.Log("Жук не найден.");
        }
        
        public void UseInput(GameInputSystem input)
        {
            var forward = 0f;
            var right = 0f;

            forward = input.GetAxis(GameplayAxis.MoveZ);
            right = input.GetAxis(GameplayAxis.MoveX);

            if (_beetle != null)
            {
                _beetle.Move(forward, right);
                // _beetle.RotateY(10f * Time.fixedDeltaTime);
            }
        }
    }
}