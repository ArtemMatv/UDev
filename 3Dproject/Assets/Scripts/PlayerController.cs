using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Assets;
using UnityEngine.EventSystems;
using InventoryNS;
using Assets.Interactables;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _inventoryCapacity;
    private Inventory inventory;
    private IInteractable focus = null;
    

    private void Start()
    {
        inventory = new Inventory(_inventoryCapacity);
        inventory.Dropped += DropItem;
    }

    private void DropItem(InventoryItem obj)
    {
        var item = Resources.Load("Prefabs/weapon_" + obj.Item.ItemId) as GameObject;

        item.GetComponent<Item>()._item = obj;

        Instantiate(item,
            new Vector3(transform.position.x, 0.13f, transform.position.z),
            Quaternion.Euler(0,0,-90));
    }

    void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _animator.SetBool("isWalking", false);
        }

        if (_agent.remainingDistance <= focus?.GetRadius())
        {
            _agent.SetDestination(gameObject.transform.position);
            focus.Interact();

            if (focus is Item)
                (focus as Item).PickUpTarget = inventory;  
            
            SetFocus(null);
        }
    }

    public void Move(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _agent.SetDestination(hit.point);
            _animator.SetBool("isWalking", true);

            SetFocus(hit.collider.GetComponent<IInteractable>());
        }
    }

    void SetFocus(IInteractable interactable)
    {
        focus = interactable;
    }

    public Inventory GetInventory()
    {
        if (inventory == null)
            inventory = new Inventory(_inventoryCapacity);

        return inventory;
    }
}
