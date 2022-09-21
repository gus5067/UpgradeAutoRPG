using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControll : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    private void Awake()
    {
        player.GetComponent<NavMeshAgent>().enabled = false;
    }

    private void Start()
    {
        if (GameManager.Instance.characterPos != Vector3.zero)
        {
            player.transform.position = GameManager.Instance.characterPos;

            player.transform.rotation = Quaternion.Euler(GameManager.Instance.characterRotation);
        }
        player.GetComponent<NavMeshAgent>().enabled = true;
    }

}
