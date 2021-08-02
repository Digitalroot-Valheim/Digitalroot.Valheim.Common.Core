using Digitalroot.Valheim.Common.Core.Enums;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Digitalroot.Valheim.Common.Core.Managers
{
  public class CraftingStationsManager : AbstractManager<CraftingStationsManager>
  {
    private static Dictionary<string, CraftingStation> _craftingStations;
    private static readonly StaticSourceLogger StaticSourceLogger = new();

    private static void InitCraftingStations()
    {
      Log.Trace(StaticSourceLogger, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      if (_craftingStations == null)
      {
        _craftingStations = new Dictionary<string, CraftingStation>();
        foreach (var recipe in ObjectDB.instance.m_recipes)
        {
          if (recipe.m_craftingStation != null && !_craftingStations.ContainsKey(recipe.m_craftingStation.name))
          {
            _craftingStations.Add(recipe.m_craftingStation.name, recipe.m_craftingStation);
          }
        }
      }
    }

    protected override void OnInitialize()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Initialize<CraftingStations>();
      InitCraftingStations();
    }

    public override void ApplyCustomChanges()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
    }

    protected override void LoadCustomChanges()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      ManagerDictionary.Add(CraftingStations.None, null);
      ManagerDictionary.Add(CraftingStations.Workbench, "piece_workbench");
      ManagerDictionary.Add(CraftingStations.Forge, "forge");
      ManagerDictionary.Add(CraftingStations.CharcoalKiln, "charcoal_kiln");
      ManagerDictionary.Add(CraftingStations.BlastFurnace, "blastfurnace");
      ManagerDictionary.Add(CraftingStations.Cauldron, "piece_cauldron");
      ManagerDictionary.Add(CraftingStations.Fermenter, "fermenter");
      ManagerDictionary.Add(CraftingStations.Stonecutter, "piece_stonecutter");
      ManagerDictionary.Add(CraftingStations.ArtisanTable, "piece_artisanstation");
      ManagerDictionary.Add(CraftingStations.Windmill, "windmill");
      ManagerDictionary.Add(CraftingStations.SpinningWheel, "SpinningWheel");
    }

    //private readonly List<CraftingStation> _craftingStations = ObjectDB.instance.m_recipes.Where(w => w.m_craftingStation != null).GroupBy(t => t.m_craftingStation).Select(s => s.Key).ToList();

    public CraftingStation Get(CraftingStations craftingStation)
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      // Log.Debug($"{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      // Log.Debug($"Passed value: {craftingStation}");

      if (!_craftingStations.ContainsKey((string)ManagerDictionary[craftingStation]))
      {
        Log.Warning(this, $"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] Could not find crafting station ({craftingStation})");
        Log.Warning(this, $"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] Available Stations: {string.Join(", ", _craftingStations.Keys)}");
        return null;
      }
      return _craftingStations[(string)ManagerDictionary[craftingStation]];
    }

    [Conditional("DEBUG")]
    [UsedImplicitly]
    public void Debug()
    {
      Log.Debug(this, "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
      Log.Debug(this, $"{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");

      Log.Debug(this, "ManagerDictionary");
      foreach (KeyValuePair<Enum, object> keyValuePair in ManagerDictionary)
      {
        Log.Debug(this, $"k:{keyValuePair.Key}, v:{keyValuePair.Value}");
      }

      Log.Debug(this, "_craftingStations");
      foreach (var craftingStation in _craftingStations)
      {
        Log.Debug(this, $"k: {craftingStation.Key}, v:{craftingStation.Value}");
      }

      Log.Debug(this, "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
    }

    [UsedImplicitly]
    public static void Reset()
    {
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      _craftingStations = null;
    }
  }
}
