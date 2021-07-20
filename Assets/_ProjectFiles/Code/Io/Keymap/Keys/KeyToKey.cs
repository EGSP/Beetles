using System;
using UnityEngine;

namespace Game.Io
{
    /// <summary>
    /// Структура хранящая связь между игровым действием и способах ввода.
    /// </summary>
    public struct KeyToKey
    {
        public readonly GameplayKeyCode GameplayKey;

        public KeyCode[] KeyCodes { get; private set; }
        
        public KeyToKey(GameplayKeyCode gameplayKey, params KeyCode[] keyCodes)
        {
            GameplayKey = gameplayKey;
            KeyCodes = keyCodes ?? Array.Empty<KeyCode>();
        }

        /// <summary>
        /// Возвращает true, если хотя бы одна кнопка удерживается или нажата.
        /// </summary>
        public readonly bool Any()
        {
            for (var i = 0; i < KeyCodes.Length; i++)
            {
                var keycode = KeyCodes[i];
                if (Input.GetKeyDown(keycode) || Input.GetKey(keycode))
                    return true;
            }

            return false;
        }
        
        /// <summary>
        /// Возвращает true если ввод неактивен.
        /// </summary>
        /// <returns></returns>
        public readonly bool NotAny() => Any() == false;
    }
}