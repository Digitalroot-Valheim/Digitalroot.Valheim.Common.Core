using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Digitalroot.Valheim.Common.Core.Managers
{
  public abstract class AbstractManager<T> : Singleton<T>, ITraceableLogging where T : AbstractManager<T>, new()
  {
    // ReSharper disable once MemberCanBeProtected.Global
    public const string Namespace = "Digitalroot.Valheim.Common.Core.Managers";
    // ReSharper disable once MemberCanBeProtected.Global
    public readonly Dictionary<Enum, object> ManagerDictionary = new();

    private protected bool IsInitialized { get; set; }

    protected AbstractManager()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace(this, $"IsInitialized: {IsInitialized}");
      if (IsInitialized) return;
      Initialize();
    }

    public void Initialize()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace(this, $"IsInitialized: {IsInitialized}");
      if (IsInitialized) return;
      OnInitialize();
      LoadCustomChanges();
      IsInitialized = true;
    }

    protected void Initialize<TEnum>()
      where TEnum : Enum
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}<T>");
      Log.Trace(this, $"IsInitialized: {IsInitialized}");
      if (IsInitialized) return;
      GameObjectManager.RegisterObjects<TEnum>(ManagerDictionary);
    }

    [UsedImplicitly]
    public abstract void ApplyCustomChanges();

    protected abstract void LoadCustomChanges();

    protected virtual void OnInitialize() { }

    #region Implementation of ITraceableLogging

    /// <inheritdoc />
    public string Source => Namespace;

    #endregion
  }
}
