using Assets;
using InventoryNS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerController player;
    [SerializeField] float scanRadius;
    [SerializeField] private UI_Inventory inventory;
    private float _lastInventoryOpen;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            player.Move(_camera.ScreenPointToRay(Input.mousePosition));
        }

        if (Input.GetKey(KeyCode.I) && Time.time - _lastInventoryOpen > 0.25f)
        {
            inventory.Enable();
            _lastInventoryOpen = Time.time;
        }
    }
}
