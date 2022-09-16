using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    public GameObject damageTextPrefab;

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
