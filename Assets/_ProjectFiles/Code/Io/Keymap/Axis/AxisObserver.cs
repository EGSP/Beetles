using UnityEngine;

namespace Game.Io
{
    /// <summary>
    /// Определяет состояние оси.
    /// </summary>
    public class AxisObserver
    {
        public readonly Axis Axis;

        public GameplayAxis GameplayAxis => Axis.GameplayAxis;

        public AxisObserver(Axis axis)
        {
            Axis = axis;
        }

        /// <summary>
        /// Получение значения ввода оси.
        /// </summary>
        public float Get()
        {
            return Input.GetAxisRaw(Axis.Name);
        }
    }
}