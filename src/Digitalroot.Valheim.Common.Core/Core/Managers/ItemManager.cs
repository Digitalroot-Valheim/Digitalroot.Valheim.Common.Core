using System.Reflection;
using Digitalroot.Valheim.Common.Core.Enums;

namespace Digitalroot.Valheim.Common.Core.Managers
{
  public class ItemManager : AbstractManager<ItemManager>
  {
    protected override void OnInitialize()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Initialize<DefaultItems>();
    }

    public override void ApplyCustomChanges()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
    }

    protected override void LoadCustomChanges()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      ManagerDictionary.Add(DefaultItems.YagluthThing, "YagluthDrop");
    }
  }
}
