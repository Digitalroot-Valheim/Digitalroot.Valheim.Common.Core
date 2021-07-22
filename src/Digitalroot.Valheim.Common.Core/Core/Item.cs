using Digitalroot.Valheim.Common.Core.Enums;
using System;

namespace Digitalroot.Valheim.Common.Core
{
  [Serializable]
  public class Item
  {
    public Item(DefaultItems item)
    {
      Name = item.ToString();
      ItemId = GameObjectManager.Get(item);
    }

    public string Name { get; set; }
    public string ItemId { get; set; }
  }
}
