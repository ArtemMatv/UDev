using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Assets;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask interactables;
    
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    _agent.SetDestination(hit.point);
                    _animator.SetBool("isWalking", true);
                }
            }
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _animator.SetBool("isWalking", false);
            }
        }
    }

    //This is simplified realization, but it does all the reqired work
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<IInteractable>()?.Interact();
    }
}
