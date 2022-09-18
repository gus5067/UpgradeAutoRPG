using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    [SerializeField]
    public GameObject player;


    private void Start()
    {
   

        if (GameManager.Instance.characterPos != Vector3.zero)
        {
            player.transform.position = GameManager.Instance.characterPos;

            player.transform.rotation = Quaternion.Euler(GameManager.Instance.characterRotation);
        }
    }

}
