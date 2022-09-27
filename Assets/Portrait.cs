using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portrait : MonoBehaviour
{

    [SerializeField]
    private Player player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player.onChangeHp += OnChangeHp;
    }

    public void OnChangeHp(int damage)
    {
        animator.SetTrigger("Hit");
    }
}
