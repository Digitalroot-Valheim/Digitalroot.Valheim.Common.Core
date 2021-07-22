using Digitalroot.Valheim.Common.Core.Enums;
using System;
using System.Collections.Generic;

namespace Digitalroot.Valheim.Common.Core
{
  [Serializable]
  public abstract class AbstractRecipe
  {
    private protected AbstractRecipe() { }
    public virtual string Name { get; private protected set; }
    public Recipes RecipeName { get; private protected set; }
    public Item Item { get; private protected set; }
    public int Quantity { get; private protected set; }
    public CraftingStations CraftingStation { get; private protected set; }
    public CraftingStations RepairStation { get; private protected set; }
    public bool Enabled { get; private protected set; }
    public int MinStationLevel { get; private protected set; }
    public int MaxQuality { get; private protected set; }
    public List<RecipeRequirement> Resources { get; private protected set; }

    public static AbstractRecipe CreateInstance<T>(
      Recipes recipe,
      Item item,
      int quantity,
      CraftingStations craftingStation,
      List<RecipeRequirement> recipeRequirements,
      int minStationLevel = 1,
      CraftingStations repairStation = CraftingStations.None,
      int maxQuality = 1,
      bool enabled = true
    ) where T : AbstractRecipe, new()
    {
      return new T
      {
        RecipeName = recipe,
        Item = item,
        Quantity = quantity,
        MaxQuality = maxQuality,
        CraftingStation = craftingStation,
        RepairStation = repairStation,
        Enabled = enabled,
        MinStationLevel = minStationLevel,
        Resources = recipeRequirements,
      };
    }
  }
}
