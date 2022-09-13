using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpController : MonoBehaviour
{
    public float hp;

    public float initHp;

    public GameObject hpBarPrefab;

    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);

    private Canvas uiCanvas;

    private Slider hpSlider;

    private Player hpPlayer;
    private void Awake()
    {
        hpPlayer = GetComponent<Player>();
        hpPlayer.onChangeHp += OnChangeHp;
    }
    private void Start()
    {
        SetHpBar();
    }


    private void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();

        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);

        hpSlider = hpBar.GetComponent<Slider>();

        var _hpBar = hpBar.GetComponent<EnemyHpBar>();

        _hpBar.targetTr = this.gameObject.transform;

        _hpBar.offset = hpBarOffset;
    }

    private void OnChangeHp(float hp)
    {
        this.hp = hp;
        hpSlider.value = this.hp/this.initHp;
    }
    

}
