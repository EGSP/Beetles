using UnityEngine;

namespace Game.Io
{
    public class TestInput : MonoBehaviour, IInputUser
    {
        public void UseInput(GameInputSystem input)
        {
            if (input.GetState(GameplayKeyCode.Up, KeyState.Hold))
                Debug.Log(true);
        }
    }
}