using Egsp.Core;

namespace Game.Ai
{
    /// <summary>
    /// Отношение к колонии от 0 до 100.
    /// </summary>
    public class Relation
    {
        private int _value;
        
        /// <summary>
        /// Колония-носитель данного отношения.
        /// </summary>
        public NotNone<Colony> Owner { get; private set; }

        /// <summary>
        /// Колония, к которой применимо отношение.
        /// </summary>
        public NotNone<Colony> Colony { get; private set; }

        /// <summary>
        /// Числовой показатель отношения.
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                if (value < 0)
                {
                    _value = 0;
                    return;
                }
                else if (value > 100)
                {
                    _value = 100;
                    return;
                }

                _value = value;
            }
        }

        public Relation(NotNone<Colony> owner, NotNone<Colony> colony, int value)
        {
            Owner = owner;
            Colony = colony;
            Value = value;
        }
    }
}