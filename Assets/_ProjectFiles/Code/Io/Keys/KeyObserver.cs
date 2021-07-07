using System;

namespace Game.Io
{
    /// <summary>
    /// Определяет состояние нажатия ключа. 
    /// </summary>
    public sealed class KeyObserver
    {
        public readonly KeyToKey Key;
        
        private readonly float _holdTreshold;

        private float _holdTime;
        
        public KeyState KeyState { get; private set; }

        public GameplayKeyCode GameplayKey => Key.GameplayKey;

        public KeyObserver(KeyToKey key, float holdTreshold)
        {
            Key = key;
            _holdTreshold = holdTreshold;
            
            KeyState = KeyState.Inactive;
        }

        public void Observe(float deltaTime)
        {
            // Вызываем метод состояния, который проверяет некоторую логику.
            switch (KeyState)
            {
                case KeyState.Inactive:
                    Inactive(Key.Any(), deltaTime);
                    break;
                case KeyState.Down:
                    Down(Key.Any(), deltaTime);
                    break;
                case KeyState.Hold:
                    Hold(Key.Any(), deltaTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        // Состояния ключа.

        private void Inactive(bool any, float deltaTime)
        {
            // Ключ нажат.
            if (any)
            {
                SwitchState(KeyState.Down);
            }
        }
        
        private void Down(bool any, float deltaTime)
        {
            // Ключ неактивен.
            if (!any)
            {
                SwitchState(KeyState.Inactive);
            }
            else // Ключ все еще нажат.
            {
                _holdTime += deltaTime;
                if (_holdTime >= _holdTreshold)
                {
                    SwitchState(KeyState.Hold);
                }
            }
        }

        private void Hold(bool any, float deltaTime)
        {
            // Здесь смысла считать время удержания смысла нет, ведь
            // состояние уже активно.
            
            // Ключ неактивен.
            if (!any)
            {
                _holdTime = 0;
                SwitchState(KeyState.Inactive);
            }
        }

        private void SwitchState(KeyState newState)
        {
            KeyState = newState;
        }
    }
}