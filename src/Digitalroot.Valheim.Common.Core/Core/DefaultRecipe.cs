using System;

namespace Digitalroot.Valheim.Common.Core
{
  [Serializable]
  public class DefaultRecipe : AbstractRecipe
  {
    public override string Name => $"Recipe_{RecipeName}_Digitalroot";
  }
}
