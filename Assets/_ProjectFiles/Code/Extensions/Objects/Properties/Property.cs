namespace Game.Extensions
{
    public abstract class Property
    {
        public abstract PropertyId Id { get; }
    }
    
    public abstract class Property<T> : Property
    {
        /// <summary>
        /// Статический идентификатор для свойства.
        /// </summary>
        public static readonly PropertyId StaticId;

        public override PropertyId Id => StaticId;

        static Property()
        {
            StaticId = typeof(T);
        }
    }
}