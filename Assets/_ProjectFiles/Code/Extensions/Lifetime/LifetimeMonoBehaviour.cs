using System;
using Egsp.Core;
using JetBrains.Lifetimes;
using UnityEngine;

namespace Game.Extensions
{
    public abstract class LifetimeMonoBehaviour : MonoBehaviour, ILifetime
    {
        /// <summary>
        /// Ключ к управлению времени жизни объекта.
        /// </summary>
        private LifetimeDefinition _lifetimeDefinition = new LifetimeDefinition();

        // Изначально формат ничему не равен. Позже он будет определен с помощью атрибута или вручную.
        protected Option<TerminateFormat> _format = Option<TerminateFormat>.None;

        public Lifetime Lifetime => _lifetimeDefinition != null ?
            _lifetimeDefinition.Lifetime : (_lifetimeDefinition = new LifetimeDefinition()).Lifetime;

        public TerminateFormat Format
        {
            get
            {
                // Формат не был определен ни разу.
                if (_format.IsNone)
                {
                    // Попытка получить формат из атрибута.
                    _format = TerminateFormatFromAttribute();
                }

                return _format.Value;
            }
            set => _format = value;
        }

        /// <summary>
        /// Завершает жизнь объекта.
        /// Он будет уничтожен или отключен для дальнейшего использования в зависимости от настроек.
        /// </summary>
        protected void Terminate()
        {
            if (_lifetimeDefinition == null)
                return;
            
            // Если объект живой.
            if (_lifetimeDefinition.Status == LifetimeStatus.Alive)
            {
                _lifetimeDefinition.Terminate();
                _lifetimeDefinition = null;

                switch (Format)
                {
                    case TerminateFormat.Deactivate:
                        gameObject.SetActive(false);
                        break;
                    case TerminateFormat.Destroy:
                        Destroy(gameObject);
                        break;
                }
            }
        }

        protected TerminateFormat TerminateFormatFromAttribute()
        {
            // Определяем формат для всех типов-наследников.
            var formatAttribute = (TerminateFormatAttribute)Attribute.
                GetCustomAttribute(GetType(), typeof(TerminateFormatAttribute));

            if (formatAttribute == null)
            {
                return TerminateFormat.Destroy;
            }
            else
            {
                return formatAttribute.Format;   
            }
        }
    }
}