using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ViewDetector : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    [Header("View Detector")]
    [SerializeField]
    private float viewRadius;
    [SerializeField , Range(0f, 360f)]
    private float viewAngle;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private LayerMask obstacleMask;


    public void FindTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        if(targets.Length < 1)
        {
            target = null;
            return;
        }
        for(int i = 0; i < targets.Length; i++)
        {
            Vector3 dirToTarget = (targets[i].transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad))
            {
                Debug.Log("타겟이 시야 밖");
                continue; // 내적값이 작다는 것은 시야각보다 밖에 있다는 것 (코사인은 각도가 커질수록 값이 작아짐)
            }

            //float distToTarget = Vector3.Distance(transform.position, targets[i].transform.position);
            //if(Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
            //{
            //    Debug.Log("타겟이 장애물 뒤");
            //    target = null;
            //    continue;// 레이캐스트를 쏴서 타겟에 닿기 전에 장애물에 닿으면 타겟이 장애물 뒤에 있다는 것
            //}
            ////Debug.DrawRay(transform.position, dirToTarget * distToTarget, Color.red);

            target = targets[i].gameObject;
            return;
        }
    }

    public List<Collider> FindTargets(float Radius, float Angle)
    {
        List<Collider> skillTargets = new List<Collider>();
        Collider[] targets = Physics.OverlapSphere(transform.position, Radius, targetMask);
        if (targets.Length < 1)
        {
            return null;
        }
        for (int i = 0; i < targets.Length; i++)
        {
            Vector3 dirToTarget = (targets[i].transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(Angle * 0.5f * Mathf.Deg2Rad))
            {
                Debug.Log("타겟이 시야 밖");
                continue; // 내적값이 작다는 것은 시야각보다 밖에 있다는 것 (코사인은 각도가 커질수록 값이 작아짐)
            }
            skillTargets.Add(targets[i]);
        }
        return skillTargets;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f);


        Debug.DrawRay(transform.position, lookDir * viewRadius, Color.green);
        Debug.DrawRay(transform.position, rightDir * viewRadius, Color.yellow);
        Debug.DrawRay(transform.position, leftDir * viewRadius, Color.yellow);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
