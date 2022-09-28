using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Portrait : MonoBehaviour
{

    [SerializeField]
    private Player player;
    private Animator animator;
    [SerializeField]
    private Image Background;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player.onChangeHp += OnChangeHp;
    }

    public void OnChangeHp(int damage)
    {
        if(player.hpController.hp > 0)
        {
            animator.SetTrigger("Hit");
            StartCoroutine(BackgroundColor());
        }
        else
        {
            animator.SetTrigger("Die");
            animator.SetBool("isDie", true);
            Background.color = Color.black;
        }
      
    }

    IEnumerator BackgroundColor()
    {
        Background.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        Background.color = Color.white;
    }
}
