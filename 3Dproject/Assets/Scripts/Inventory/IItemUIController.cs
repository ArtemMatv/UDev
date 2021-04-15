using InventoryNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public interface IItemUIController : IPointerClickHandler
{
    int Position { get; set; }
    InventoryItem GetItem();
    void SetItem(InventoryItem item, bool updateBG = true);
    Action<IItemUIController> OnLeftClick { get; set; }
    Action<InventoryItem> OnRightClick { get; set; }
    void BackBackground();
}

