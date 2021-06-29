namespace Game.Extensions
{
    /// <summary>
    /// Объект, которому нужен PrefabProvider.
    /// </summary>
    public interface IPrefabUser
    {
        IPrefabProvider PrefabProvider { get; set; }
    }
}