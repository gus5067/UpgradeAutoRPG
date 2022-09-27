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
        animator.SetTrigger("Hit");
        StartCoroutine(BackgroundColor());
    }

    IEnumerator BackgroundColor()
    {
        Background.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        Background.color = Color.white;
    }
}
