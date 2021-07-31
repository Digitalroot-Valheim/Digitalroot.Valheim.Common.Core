using Digitalroot.Valheim.Common.Core.Enums;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Digitalroot.Valheim.Common.Core.Managers
{
  public class RecipesManager : AbstractManager<RecipesManager>
  {
    #region AbstractManager
    protected override void OnInitialize()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      Initialize<Recipes>();
    }

    public override void ApplyCustomChanges()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      foreach (var keyValuePair in ManagerDictionary)
      {
        if (keyValuePair.Value is AbstractRecipe recipe)
        {
          Log.Debug(this, $"Trying to create: {recipe.RecipeName}");
          var r = CreateRecipe(recipe);
          if (r == null)
          {
            Log.Debug(this, $"[{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}] Failed to create recipe ({recipe.RecipeName})");
            continue;
          }
          AddNewRecipe(r);
        }
      }
    }

    protected override void LoadCustomChanges()
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
    }

    #endregion

    public void AddRecipe(AbstractRecipe abstractRecipe)
    {
      Log.Trace(this, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      if (ManagerDictionary.ContainsKey(abstractRecipe.RecipeName)) return;
      ManagerDictionary.Add(abstractRecipe.RecipeName, abstractRecipe);
    }

    private static void AddNewRecipe(global::Recipe recipe)
    {
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      if (ObjectDB.instance.m_recipes.RemoveAll(x => x.name == recipe.name) > 0)
      {
        Log.Debug(Instance, $"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] Removed recipe ({recipe.name})");
      }

      ObjectDB.instance.m_recipes.Add(recipe);
      Log.Debug(Instance, $"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] Added recipe: {recipe.name}");
    }

    private static global::Recipe CreateRecipe(AbstractRecipe abstractRecipe)
    {
      Log.Trace(Instance, $"{Namespace}.{MethodBase.GetCurrentMethod().DeclaringType?.Name}.{MethodBase.GetCurrentMethod().Name}");
      var itemPrefab = ObjectDB.instance.GetItemPrefab(abstractRecipe.Item.ItemId);
      if (itemPrefab == null)
      {
        Log.Warning(Instance, $"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] Could not find item prefab ({abstractRecipe.Item.ItemId})");
        return null;
      }

      var itemDrop = itemPrefab.GetComponent<ItemDrop>();
      if (abstractRecipe.MaxQuality > 1)
      {
        itemDrop.m_itemData.m_shared.m_maxQuality = abstractRecipe.MaxQuality;
      }

      var newRecipe = ScriptableObject.CreateInstance<global::Recipe>();
      newRecipe.name = abstractRecipe.Name;
      newRecipe.m_amount = abstractRecipe.Quantity;
      newRecipe.m_minStationLevel = abstractRecipe.MinStationLevel;
      newRecipe.m_item = itemDrop;
      newRecipe.m_enabled = abstractRecipe.Enabled;
      if (abstractRecipe.CraftingStation != CraftingStations.None)
      {
        newRecipe.m_craftingStation = GameObjectManager.Get(abstractRecipe.CraftingStation);
      }

      if (abstractRecipe.RepairStation != CraftingStations.None)
      {
        newRecipe.m_repairStation = GameObjectManager.Get(abstractRecipe.RepairStation);
      }

      var requirements = new List<Piece.Requirement>();
      foreach (var requirement in abstractRecipe.Resources)
      {
        var reqPrefab = ObjectDB.instance.GetItemPrefab(requirement.Item.ItemId);
        if (reqPrefab == null)
        {
          Log.Error(Instance, $"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] Could not find requirement item ({abstractRecipe.Name}): {requirement.Item.Name}");
          continue;
        }

        requirements.Add(new Piece.Requirement
        {
          m_amount = requirement.Quantity,
          m_resItem = reqPrefab.GetComponent<ItemDrop>(),
          m_amountPerLevel = requirement.AmountPerLevel,
        });
      }
      newRecipe.m_resources = requirements.ToArray();

      return newRecipe;
    }
  }
}
