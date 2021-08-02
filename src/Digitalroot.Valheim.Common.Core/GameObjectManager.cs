using Digitalroot.Valheim.Common.Core;
using Digitalroot.Valheim.Common.Core.Enums;
using Digitalroot.Valheim.Common.Core.Managers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Digitalroot.Valheim.Common
{
  [UsedImplicitly]
  public sealed class GameObjectManager : Singleton<GameObjectManager>, ITraceableLogging
  {
    // ReSharper disable once MemberCanBePrivate.Global
    public const string Namespace = "Digitalroot.Valheim." + nameof(Common);

    public GameObjectManager()
    {
#if DEBUG
      EnableTrace = true;
#else
      EnableTrace = false;
#endif
      Log.RegisterSource(this);
    }

    [UsedImplicitly]
    public void Initialize()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      BossesManager.Instance.Initialize();
      CraftingStationsManager.Instance.Initialize();
      EnemiesManager.Instance.Initialize();
      ItemTypesManager.Instance.Initialize();
      ItemManager.Instance.Initialize();
      RecipesManager.Instance.Initialize();
    }

    #region RegisteredObjects

    // ReSharper disable once MemberCanBePrivate.Global
    public readonly Dictionary<string, Dictionary<Enum, object>> RegisteredObjects = new();

    public static void RegisterObjects<TK>(Dictionary<Enum, object> dictionary)
      where TK : Enum
    {
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Instance.RegisteredObjects.Add(typeof(TK).ToString(), dictionary);
    }

    #endregion

    #region Helpers

    public static string Get<T>(T value) where T : Enum
    {
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace(Instance, $"{typeof(T)}:{value}");
      return Get(value, Instance.RegisteredObjects[typeof(T).ToString()]).ToString();
    }

    private static object Get<T>(T value, Dictionary<T, object> dictionary) where T : Enum
    {
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace(Instance, $"{typeof(T)}:{value}");
      Log.Trace(Instance, $"dictionary:{dictionary}");
      return dictionary.ContainsKey(value) ? dictionary[value] : value.ToString();
    }

    [UsedImplicitly]
    public static DefaultRecipe Get(Recipes value)
    {
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace(Instance, $"{value}");
      return (DefaultRecipe) Get(value, Instance.RegisteredObjects[typeof(Recipes).ToString()]);
    }

    // ReSharper disable once RedundantNameQualifier
    public static global::CraftingStation Get(CraftingStations craftingStation)
    {
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace(Instance, $"{craftingStation}");
      return CraftingStationsManager.Instance.Get(craftingStation);
    }

    #endregion

    #region Implementation of ITraceableLogging

    /// <inheritdoc />
    public string Source => Namespace;

    /// <inheritdoc />
    public bool EnableTrace { get; }

    #endregion
  }
}
