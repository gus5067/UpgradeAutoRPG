using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailChanger : MonoBehaviour
{
    [SerializeField]
    private TrailData trail;

    private TrailRenderer trailRenderer;
    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        switch (WeaponManager.Instance.WeaponStateNum)
        {
            case 0:
                trailRenderer.material = trail.trails[0];
                break;
            case 1:
                trailRenderer.material = trail.trails[1];
                break;
            case 2:
                trailRenderer.material = trail.trails[2];
                break;
        }

    }
}
