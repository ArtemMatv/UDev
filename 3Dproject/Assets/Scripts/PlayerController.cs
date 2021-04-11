using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Assets;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    private IInteractable focus = null;

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
}
