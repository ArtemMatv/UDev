using Assets;
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
    [SerializeField] private LayerMask interactables;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            player.Move(_camera.ScreenPointToRay(Input.mousePosition));
        }
    }
}
