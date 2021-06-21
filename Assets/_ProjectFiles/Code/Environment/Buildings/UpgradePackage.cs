using Game.Entities.Resources;
using Game.Extensions;

namespace Game.Environment.Buildings
{
    /// <summary>
    /// Содержит в себе данные о требуемых ресурсах для улучшения.
    /// </summary>
    public class UpgradePackage
    {
        public readonly Count<ResourceRarity> Common;
        public readonly Count<ResourceRarity> Rare;
        public readonly Count<ResourceRarity> Epic;
        
        /// <param name="common">Количество обычного ресурса.</param>
        /// <param name="rare">Количество редкого ресурса.</param>
        /// <param name="epic">Количество эпического ресурса.</param>
        public UpgradePackage(int common, int rare, int epic)
        {
            Common = new Count<ResourceRarity>(ResourceRarity.Common, common);
            Rare = new Count<ResourceRarity>(ResourceRarity.Rare, rare);
            Epic = new Count<ResourceRarity>(ResourceRarity.Epic, epic);
        }
    }
}