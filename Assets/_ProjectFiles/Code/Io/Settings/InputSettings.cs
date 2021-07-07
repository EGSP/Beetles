using System.Collections.Generic;
using UnityEngine;

namespace Game.Io
{
    /// <summary>
    /// Хранит информацию о системе ввода.
    /// </summary>
    public sealed class InputSettings
    {
        public const float Treshold = 0.15f;
        
        /// <summary>
        /// Все используемые ключи.
        /// </summary>
        public readonly List<KeyToKey> Keys;

        /// <summary>
        /// Временной порог для перехода в состояние удерживания ключа.
        /// </summary>
        public readonly float HoldTreshold;

        public InputSettings(List<KeyToKey> keys, float holdTreshold)
        {
            Keys = keys ?? new List<KeyToKey>();
            HoldTreshold = holdTreshold;
        }

        public static InputSettings PcSettings()
        {
            var keys = new List<KeyToKey>();
            keys.Clear();
            // WASD
            keys.Add(new KeyToKey(GameplayKeyCode.Up, KeyCode.W));
            keys.Add(new KeyToKey(GameplayKeyCode.Down, KeyCode.S));
            keys.Add(new KeyToKey(GameplayKeyCode.Left,KeyCode.A));
            keys.Add(new KeyToKey(GameplayKeyCode.Right, KeyCode.D));
            
            // Mouse
            keys.Add(new KeyToKey(GameplayKeyCode.MouseLeft, KeyCode.Mouse0));
            keys.Add(new KeyToKey(GameplayKeyCode.MouseRight, KeyCode.Mouse1));
            keys.Add(new KeyToKey(GameplayKeyCode.MouseMiddle, KeyCode.Mouse2));

            var treshold = Treshold;
            return new InputSettings(keys, treshold);
        }
    }
}