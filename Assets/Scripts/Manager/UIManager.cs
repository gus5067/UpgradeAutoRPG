using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public GameObject damageTextPrefab;

    [SerializeField]
    public GameObject playerDamageTextPrefab;

    [SerializeField]
    public Canvas uiCanvas;

    [SerializeField]
    public TextMeshProUGUI stageResultText;

    [SerializeField]
    public TextMeshProUGUI stageRoundText;
    private void Start()
    {
        stageResultText.text = " ";
    }
}
