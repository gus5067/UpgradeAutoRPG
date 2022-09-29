using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilRoar : StateMachineBehaviour
{
    private EvilWorrior worrior;
    private Collider[] colliders;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        worrior = animator.GetComponentInParent<EvilWorrior>();
        worrior.skillRoar.SetActive(true);
        colliders = Physics.OverlapSphere(worrior.transform.position + Vector3.up, 7, 1 << 7);
        foreach (var target in colliders)
        {
            target.GetComponent<Player>().HitDamage(60);
        }
        worrior.HitDamage(-60 * colliders.Length);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        worrior.skillRoar.SetActive(false);
    }
}
