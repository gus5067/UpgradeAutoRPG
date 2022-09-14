using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class damageText : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectDamage;

    [HideInInspector]
    public Vector3 offset = Vector3.zero;

    [HideInInspector]
    public Transform targetTr;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float destroyTime = 5f;
    // Start is called before the first frame update
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();

        uiCamera = canvas.worldCamera;

        rectParent = canvas.GetComponent<RectTransform>();
        rectDamage = this.gameObject.GetComponent<RectTransform>();

        Destroy(gameObject, destroyTime);

        if (targetTr != null)
        {
            var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);

            if (screenPos.z < 0.0f)
            {
                screenPos *= -1.0f;
            }

            var localPos = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);

            rectDamage.localPosition = localPos;
        }
    }

    private void Update()
    {
        gameObject.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }

}
