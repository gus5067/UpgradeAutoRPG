using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private GameObject FireBallEffect;
    private ViewDetector viewDetector;
    private MeshRenderer meshRenderer;

    private bool isReady = false;

    private void Awake()
    {
        viewDetector = GetComponent<ViewDetector>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        StartCoroutine(FireBallRoutine());
    }

    private void Update()
    {
        if(isReady)
        {
            FireBallMove();
        }
        else
        {
            return;
        }
    }
    public void Increase(float i)
    {
        gameObject.transform.localScale = Vector3.one * i;
    }

    IEnumerator FireBallRoutine()
    {
        
        for(float i = 0.5f; i < 3f; i += 0.02f)
        {
            Increase(i);
            yield return new WaitForSeconds(0.01f);
        }
        isReady = true;
    }

    private void FireBallMove()
    {
        viewDetector.FindTarget();
        transform.position = Vector3.MoveTowards(transform.position, viewDetector.target.transform.position, Time.deltaTime * 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<IDamageable>().HitDamage(50);
            FireBallEffect.SetActive(true);
            meshRenderer.enabled = false;
            Destroy(gameObject,1f);
        }
    }
}
