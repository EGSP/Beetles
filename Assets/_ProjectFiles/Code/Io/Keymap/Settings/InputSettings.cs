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

        public readonly List<Axis> Axes;

        public InputSettings(List<KeyToKey> keys, float holdTreshold, List<Axis> axes)
        {
            Keys = keys ?? new List<KeyToKey>();
            HoldTreshold = holdTreshold;
            Axes = axes ?? new List<Axis>();
        }

        public static InputSettings PcSettings()
        {
            // Keys
            var keys = new List<KeyToKey>();
            
            keys.Add(new KeyToKey(GameplayKeyCode.Up, KeyCode.W));
            keys.Add(new KeyToKey(GameplayKeyCode.Down, KeyCode.S));
            keys.Add(new KeyToKey(GameplayKeyCode.Left,KeyCode.A));
            keys.Add(new KeyToKey(GameplayKeyCode.Right, KeyCode.D));
            
            
            keys.Add(new KeyToKey(GameplayKeyCode.Select, KeyCode.Mouse0));
            keys.Add(new KeyToKey(GameplayKeyCode.Option, KeyCode.Mouse1));
            keys.Add(new KeyToKey(GameplayKeyCode.AdditionalOption, KeyCode.Mouse2));

            // Hold settings
            var treshold = Treshold;

            // Axes
            var axes = new List<Axis>();

            axes.Add(new Axis(GameplayAxis.MoveX, "Horizontal"));
            axes.Add(new Axis(GameplayAxis.MoveZ, "Vertical"));
            axes.Add(new Axis(GameplayAxis.ViewX, "MouseX"));
            axes.Add(new Axis(GameplayAxis.ViewY, "MouseY"));

            return new InputSettings(keys, treshold, axes);
        }
    }
}