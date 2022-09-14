using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTextController : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private TextMeshProUGUI gemText;

    private void Start()
    {
        GameManager.instance.onChangeMoney += OnChangeMoney;
        moneyText.text = GameManager.instance.gameMoney.ToString();
        gemText.text = GameManager.instance.gameGem.ToString();
    }

    private void OnChangeMoney()
    {
        moneyText.text = GameManager.instance.gameMoney.ToString();
        gemText.text = GameManager.instance.gameGem.ToString(); 
    }
}
