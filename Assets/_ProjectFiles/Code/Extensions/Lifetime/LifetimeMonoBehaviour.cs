using System;
using Egsp.Core;
using JetBrains.Lifetimes;
using UnityEngine;

namespace Game.Extensions
{
    /// <summary>
    /// В методе Awake и Start
    /// </summary>
    public abstract class LifetimeMonoBehaviour : MonoBehaviour, ILifetime
    {
        /// <summary>
        /// Родительский лайфтайм, который может быть установлен извне.
        /// Нужен чтобы привязать данный объект к другому. 
        /// </summary>
        private Option<Lifetime> _parentLifetime = Option<Lifetime>.None; 
        
        /// <summary>
        /// Ключ к управлению времени жизни объекта. Именно на этот лайфтайм привязываются
        /// другие объекты.
        /// </summary>
        private LifetimeDefinition _lifetimeDefinition = new LifetimeDefinition();

        // Изначально формат ничему не равен. Позже он будет определен с помощью атрибута или вручную.
        private Option<TerminateFormat> _format = Option<TerminateFormat>.None;

        public Lifetime Lifetime => _lifetimeDefinition != null ?
            _lifetimeDefinition.Lifetime : Alive().Lifetime;

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

        public bool IsTerminated => _lifetimeDefinition == null;

        /// <summary>
        /// Завершает жизнь объекта.
        /// Он будет уничтожен или отключен для дальнейшего использования в зависимости от настроек.
        /// </summary>
        protected void Terminate()
        {
            if (IsTerminated)
                return;
            
            // Если объект живой.
            if (_lifetimeDefinition.Status == LifetimeStatus.Alive)
            {
                // Терминируем все дочерние лайфтаймы.
                _lifetimeDefinition.Terminate();
                // Очищаем ссылку, т.к. позже мы обновим лайфтайм.
                _lifetimeDefinition = null;
                
                // Избавляемся от родительского лайфтайма, чтобы в дальнейшем можно было установить 
                // нового родителя, если наш объект не будет уничтожаться.
                _parentLifetime = Option<Lifetime>.None;

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

        /// <summary>
        /// Возобновляет лайфтайм объекта, если это возможно.
        /// Возвращает ссылку на уже обновленный лайфтайм.
        /// </summary>
        protected LifetimeDefinition Alive()
        {
            _lifetimeDefinition = new LifetimeDefinition();
            
            OnAlive();
            
            return _lifetimeDefinition;
        }
        
        /// <summary>
        /// Вызывается после восстановления лайфтайма. При создании объекта вызова не произойдет.
        /// </summary>
        protected virtual void OnAlive(){}

        /// <summary>
        /// Устанавливает для объекта родительский лайфтайм.
        /// Метод появился по причине недоступности конструктора объекта MonoBehaviour.
        /// </summary>
        /// <exception cref="InvalidOperationException">Нельзя установить родительский лайфтайм, если он был установлен ранее.</exception>
        public void Parent(Lifetime parentLifetime)
        {
            if (_parentLifetime.IsSome)
                throw new InvalidOperationException("Нельзя установить родительский лайфтайм, если он был установлен ранее.");
            
            // Создаем лайфтайм, связанный с родительским.
            var parentedLifetimeDefinition = parentLifetime.CreateNested();
            // Нам дефинишн не нужен, потому что у нас есть свой собственный.
            // Можно было бы заменить свой на новый, однако в момент создания объекта,
            // Awake или Start на наш лайфтайм может кто-нибудь подписаться.
            // Можно было бы сделать рекомендацию, но ее же никто не будет соблюдать :).
            // Поэтому контроллируем два лайфтайма.
            _parentLifetime = parentedLifetimeDefinition.Lifetime;
            // Терминируем наш объект, если родительский объект начал терминацию.
            _parentLifetime.Value.OnTermination(Terminate);
        }

        /// <summary>
        /// Изменяет формат терминации.
        /// </summary>
        public void ChangeTerminateFormat(TerminateFormat format)
        {
            _format = format;
        }
    }
}