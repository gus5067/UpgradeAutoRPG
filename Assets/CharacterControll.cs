using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    [SerializeField]
    public Transform player;

    private void Start()
    {
        if(GameManager.Instance.characterPos != null)
        {
            player.transform.position = GameManager.Instance.characterPos;

            player.transform.rotation = Quaternion.Euler(GameManager.Instance.characterRotation);
        }
    }
}
