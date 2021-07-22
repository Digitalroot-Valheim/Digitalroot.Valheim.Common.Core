using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Digitalroot.Valheim.Common.Core.Managers
{
  public abstract class AbstractManager<T> : Singleton<T> where T : AbstractManager<T>, new()
  {
    public const string Namespace = "Digitalroot.Valheim.Common.Core.Managers";
    public Dictionary<Enum, object> ManagerDictionary = new Dictionary<Enum, object>();

    private protected bool IsInitialized { get; set; }
    
    protected AbstractManager()
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace($"IsInitialized: {IsInitialized}");
      if (IsInitialized) return;
      Initialize();
    }

    public void Initialize()
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace($"IsInitialized: {IsInitialized}");
      if (IsInitialized) return;
      OnInitialize();
      LoadCustomChanges();
      IsInitialized = true;
    }

    protected void Initialize<TEnum>()
      where TEnum : Enum
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}<T>");
      Log.Trace($"IsInitialized: {IsInitialized}");
      if (IsInitialized) return;
      GameObjectManager.RegisterObjects<TEnum>(ManagerDictionary);
    }

    [UsedImplicitly]
    public abstract void ApplyCustomChanges();

    protected abstract void LoadCustomChanges();

    protected virtual void OnInitialize() { }
  }
}
