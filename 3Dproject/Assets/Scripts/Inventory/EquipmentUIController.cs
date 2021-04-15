using InventoryNS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.PointerEventData;

public class EquipmentUIController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image background;
    [SerializeField] private Sprite defaultBackground;
    public EquipmentType EquipmentType;
    public int Position { get; set; }
    public InventoryItem Item;
    public Action<InventoryItem> OnLeftClick { get; set; }
    public Action<InventoryItem> OnRightClick { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        /*if (eventData.button == InputButton.Left)
        {
            OnLeftClick(Item);
            background.sprite = defaultBackground;
        }*/

        if (eventData.button == InputButton.Right)
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
}
