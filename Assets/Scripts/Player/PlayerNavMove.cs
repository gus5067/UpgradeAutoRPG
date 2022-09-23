using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMove : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    LayerMask interactLayerMask;

    private NavMeshAgent character;

    private Animator animator;

    private Vector3 destination;

    [SerializeField]
    private Transform interactPoint;

    [SerializeField, Range(0f, 3f)]
    private float interactRange;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        character = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MoveToMousePoint();
        InteractSphere();
    }
    private void MoveToMousePoint()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                destination = hit.point;
            }
        }
        character.SetDestination(destination);

        if(character.velocity.magnitude > 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }


    private void InteractSphere()
    {
        Collider[] targets = Physics.OverlapSphere(interactPoint.position, interactRange, interactLayerMask);

        if (targets.Length > 0)
        {
            IInteractable interactTarget = targets[0].gameObject.GetComponent<IInteractable>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactTarget?.Interact();
            }

        }
        else
        {
            return;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPoint.position, interactRange);
    }
}
