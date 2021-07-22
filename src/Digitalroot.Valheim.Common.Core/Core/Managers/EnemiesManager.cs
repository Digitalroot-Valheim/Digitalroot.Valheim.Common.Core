using System.Reflection;
using Digitalroot.Valheim.Common.Core.Enums;

namespace Digitalroot.Valheim.Common.Core.Managers
{
  public class EnemiesManager : AbstractManager<EnemiesManager>
  {
    protected override void OnInitialize()
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Initialize<Enemies>();
    }

    public override void ApplyCustomChanges()
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
    }

    protected override void LoadCustomChanges()
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      ManagerDictionary.Add(Enemies.BoarPiggy, "Boar_piggy");
      ManagerDictionary.Add(Enemies.Drake, "Hatchling");
      ManagerDictionary.Add(Enemies.DraugrElite, "Draugr_Elite");
      ManagerDictionary.Add(Enemies.DraugrRanged, "Draugr_Ranged");
      ManagerDictionary.Add(Enemies.GreydwarfElite, "Greydwarf_Elite");
      ManagerDictionary.Add(Enemies.GreydwarfShaman, "Greydwarf_Shaman");
      ManagerDictionary.Add(Enemies.SkeletonPoison, "Skeleton_Poison");
      ManagerDictionary.Add(Enemies.WolfCub, "Wolf_cub");
    }
  }
}
