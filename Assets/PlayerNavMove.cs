using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMove : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    private NavMeshAgent character;

    private Animator animator;

    private Vector3 destination;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        character = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MoveToMousePoint();
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
}
