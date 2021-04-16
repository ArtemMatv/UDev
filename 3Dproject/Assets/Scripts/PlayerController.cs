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
    [SerializeField] private Transform helmetPosition;
    [SerializeField] private Transform weaponPosition;
    private Inventory inventory;
    private IInteractable focus = null;

    private GameObject helmet;
    private GameObject weapon;

    internal void SetEquipment(GameObject obj, EquipmentType type)
    {
        
        switch (type)
        {
            case EquipmentType.Weapon:
                if (obj != null)
                {
                    weapon = Instantiate(obj,
                       weaponPosition.position,
                       Quaternion.Euler(0, 0, -45));
                    weapon.GetComponent<Item>().EnabledOnScene(false);
                }
                else Destroy(weapon);
                break;
            case EquipmentType.Shield:
                break;
            case EquipmentType.Helmet:
                if (obj != null)
                {
                    helmet = Instantiate(obj,
                        helmetPosition.position,
                        Quaternion.Euler(0, 0, -90));
                    helmet.GetComponent<Item>().EnabledOnScene(false);
                }
                else Destroy(helmet);
                break;
            case EquipmentType.Chest:
                break;
            case EquipmentType.Leggins:
                break;
            case EquipmentType.Boots:
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        inventory = new Inventory(_inventoryCapacity);
        inventory.Dropped += DropItem;
        inventory.ChangeEquipment += SetEquipment;
    }

    private void DropItem(InventoryItem obj)
    {
        var item = Resources.Load("Prefabs/" + obj.Item.ItemId) as GameObject;

        item.GetComponent<Item>()._item = obj;

        Instantiate(item,
            new Vector3(transform.position.x, 0.13f, transform.position.z),
            Quaternion.Euler(0, 0, -90));
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

        if (helmet != null)
            helmet.transform.position = helmetPosition.position;
        if (weapon != null)
            weapon.transform.position = weaponPosition.position;
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
