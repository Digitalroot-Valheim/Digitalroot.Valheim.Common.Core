using Digitalroot.Valheim.Common.Core.Enums;
using System.Reflection;

namespace Digitalroot.Valheim.Common.Core.Managers
{
  public class BossesManager : AbstractManager<BossesManager>
  {
    protected override void OnInitialize()
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace($"IsInitialized: {IsInitialized}");
      Initialize<Bosses>();
    }

    public override void ApplyCustomChanges()
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
    }

    protected override void LoadCustomChanges()
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace($"IsInitialized: {IsInitialized}");
      ManagerDictionary.Add(Bosses.TheElder, "gd_king");
      ManagerDictionary.Add(Bosses.Moder, "Dragon");
      ManagerDictionary.Add(Bosses.Yagluth, "GoblinKing");
    }
  }
}
