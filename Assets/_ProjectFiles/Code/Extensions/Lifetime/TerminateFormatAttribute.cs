using System;

namespace Game.Extensions
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false, Inherited = false)]
    public class TerminateFormatAttribute : Attribute
    {
        public TerminateFormatAttribute()
        {
            Format = TerminateFormat.Destroy;
        }

        public TerminateFormatAttribute(TerminateFormat format)
        {
            Format = format;
        }
        
        public TerminateFormat Format { get; private set; }
    }
}