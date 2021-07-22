using System;

namespace Digitalroot.Valheim.Common.Core
{
  [Serializable]
  public class RecipeRequirement
  {
    public Item Item;
    public int Quantity;
    public int AmountPerLevel = 1;
  }
}
