using System;

namespace Egsp.Core
{
    public class NoneValueException : Exception
    {
        public NoneValueException() : base("Отсутствует значение.")
        {
        }
    }
}