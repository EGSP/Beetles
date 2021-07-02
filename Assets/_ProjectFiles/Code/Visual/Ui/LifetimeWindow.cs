using System;
using Game.Extensions;
using UnityEngine;

namespace Game.Visual.Ui
{
    [TerminateFormat(TerminateFormat.Deactivate)]
    public abstract class LifetimeWindow : LifetimeVisual
    {
        [Header("General")]
        [SerializeField] private string windowName;
        
        /// <summary>
        /// Название окна.
        /// </summary>
        public string Name => windowName;
        
        /// <summary>
        /// Текущее состояние окна.
        /// </summary>
        public WindowState State { get; private set; }

        private void Awake()
        {
            // Если окно почему-то включено при старте сцены, то мы его сразу же "открываем".
            if (gameObject.activeSelf)
                ChangeState(WindowState.Open);
        }

        /// <summary>
        /// Открывает окно.
        /// </summary>
        public void Open()
        {
            ChangeState(WindowState.Open);
        }

        /// <summary>
        /// Закрывает окно. Терминирует окно.
        /// </summary>
        public void Close()
        {
            ChangeState(WindowState.Close);
        }
        
        /// <summary>
        /// Скрывает окно. Не использует терминирование.
        /// </summary>
        public void Hide()
        {
            ChangeState(WindowState.Hide);
        }

        protected virtual void OpenInternal()
        {
            // Было ли окно ранее просто скрыто.
            if (State == WindowState.Hide)
            {
                // Просто включаем отображение.
                Enable();
                return;
            }

            Alive();  
        }

        protected virtual void CloseInternal()
        {
            // Терминируем окно, т.к. при закрытии другое поведение нам нежелательно.
            Terminate();
        }

        protected virtual void HideInternal()
        {
            // Нет смысла скрывать окно, если оно уже терминировано.
            if (IsTerminated)
                return;
            
            Disable();
        }
        

        /// <summary>
        /// Смена состояния окна.
        /// </summary>
        private void ChangeState(WindowState newState)
        {
            if (State == newState)
                return;

            switch (newState)
            {
                case WindowState.Open:
                    OpenInternal();
                    break;
                case WindowState.Close:
                    CloseInternal();
                    break;
                case WindowState.Hide:
                    HideInternal();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            // Меняем состояние только здесь, т.к. вызовы функций выше требуют проверки прошлого состояния.
            State = newState;
        }
        
        public enum WindowState
        {
            /// <summary>
            /// Окно открыто для работы.
            /// </summary>
            Open,
            /// <summary>
            /// Окно закрыто, т.е. завершило свою работу.
            /// </summary>
            Close,
            /// <summary>
            /// Окно не закрыто, но и не видно визуально. Продолжает работу.
            /// </summary>
            Hide
        }
    }
}