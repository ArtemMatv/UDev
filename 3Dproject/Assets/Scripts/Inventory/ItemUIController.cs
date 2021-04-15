using InventoryNS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.PointerEventData;

public class ItemUIController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image background;
    [SerializeField] private Sprite defaultBackground;
    public int Position { get; set; }
    public InventoryItem Item;
    public Action<ItemUIController> OnLeftClick { get; set; }
    public Action<InventoryItem> OnRightClick { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == InputButton.Left)
        {
            OnLeftClick(this);
            background.sprite = defaultBackground;
        }

        else if (eventData.button == InputButton.Right)
        {
            OnRightClick(Item);
        }
    }

    internal void SetItem(InventoryItem item, bool updateBG = true)
    {
        if (item == null)
        {
            Item = null;
            background.sprite = defaultBackground;
        }
        else
        {
            Item = item;
            item.Position = Position;

            if (updateBG)
                background.sprite = item.Item.InventoryIcon;
        }
    }

    public void BackBackground()
    {
        if (Item != null)
            background.sprite = Item.Item.InventoryIcon;
        else
            background.sprite = defaultBackground;
    }
}
