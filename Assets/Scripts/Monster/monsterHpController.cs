using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class monsterHpController : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;
    public float hp;

    public float initHp;

    public GameObject hpBarPrefab;

    private GameObject damageText;

    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);

    public Vector3 damageOffset = new Vector3(2.2f, 2.2f, 0);

    private Canvas uiCanvas;

    private Slider hpSlider;

    private TextMeshProUGUI text;
    private Monster hpMonster;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        damageText = uiManager.damageTextPrefab;
        hpMonster = GetComponent<Monster>();
        hpMonster.onChangeHp += OnChangeHp;
        
        
    }
    private void Start()
    {
        SetHpBar();
    }


    private void SetHpBar()
    {
        uiCanvas = uiManager.uiCanvas;

        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);

        hpSlider = hpBar.GetComponent<Slider>();

        var _hpBar = hpBar.GetComponent<EnemyHpBar>();

        _hpBar.targetTr = this.gameObject.transform;

        _hpBar.offset = hpBarOffset;

    }
    private void OnChangeHp(int damage)
    {
        this.hp -= damage;
        hpSlider.value = this.hp/this.initHp;
        SetDamageText(damage);
    }

    private void SetDamageText(int damageValue)
    {
        uiCanvas = uiManager.uiCanvas;

        GameObject damageObj = Instantiate<GameObject>(damageText, uiCanvas.transform);

        this.text = damageObj.GetComponent<TextMeshProUGUI>();

        var _damage = damageObj.GetComponent<damageText>();

        _damage.targetTr = this.gameObject.transform;

        _damage.offset = damageOffset;

        this.text.text = damageValue.ToString();
    }


}
