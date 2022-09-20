using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private ViewDetector viewDetector;

    private void Awake()
    {
        viewDetector = GetComponent<ViewDetector>();
    }
    private void Start()
    {
        StartCoroutine(FireBallRoutine());
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

       
    }
}
