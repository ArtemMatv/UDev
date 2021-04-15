using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemRevertDrag : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int dropAreaLayerNumber;
    public Action RightClick { get; set; }
    public Action DropItem { get; set; }
    public Action<ItemUIController> LeftClick { get; set; }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            RightClick();

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            var result = Physics2D.Raycast(Input.mousePosition, new Vector3(0, 0, 1));
            ItemUIController item = result.collider?.gameObject.GetComponent<ItemUIController>();
            LeftClick(item);

            if (item == null)
            {
                if (result.collider?.gameObject.layer == dropAreaLayerNumber)
                    DropItem();
            }
        }
    }
}
