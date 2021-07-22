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
  public sealed class GameObjectManager : Singleton<GameObjectManager>
  {
    public const string Namespace = "Digitalroot.Valheim.Common";
    
    public void Initialize()
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      BossesManager.Instance.Initialize();
      CraftingStationsManager.Instance.Initialize();
      EnemiesManager.Instance.Initialize();
      ItemTypesManager.Instance.Initialize();
      ItemManager.Instance.Initialize();
      RecipesManager.Instance.Initialize();
    }

    #region RegisteredObjects

    public readonly Dictionary<string, Dictionary<Enum, object>> RegisteredObjects = new Dictionary<string, Dictionary<Enum, object>>();

    public static void RegisterObjects<TK>(Dictionary<Enum, object> dictionary)
      where TK : Enum
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Instance.RegisteredObjects.Add(typeof(TK).ToString(), dictionary);
    }

    #endregion

    #region Helpers

    public static string Get<T>(T value) where T : Enum
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace($"{typeof(T)}:{value}");
      return Get(value, Instance.RegisteredObjects[typeof(T).ToString()]).ToString();
    }

    private static object Get<T>(T value, Dictionary<T, object> dictionary) where T : Enum
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace($"{typeof(T)}:{value}");
      Log.Trace($"dictionary:{dictionary}");
      return dictionary.ContainsKey(value) ? dictionary[value] : value.ToString();
    }

    public static DefaultRecipe Get(Recipes value)
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace($"{value}");
      return (Core.DefaultRecipe) Get(value, Instance.RegisteredObjects[typeof(Recipes).ToString()]);
    }

    public static global::CraftingStation Get(CraftingStations craftingStation)
    {
      Log.Trace($"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Log.Trace($"{craftingStation}");
      return CraftingStationsManager.Instance.Get(craftingStation);
    }

    #endregion
  }
}
