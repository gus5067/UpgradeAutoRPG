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

    public float preHp;

    public float mp;

    public float initHp;

    public float initMp;

    public GameObject hpBarPrefab;

    public GameObject mpBarPrefab;

    private GameObject damageText;

    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    public Vector3 mpBarOffset = new Vector3(0, 1.8f, 0);
    public Vector3 damageOffset = new Vector3(2.2f, 2.2f, 0);

    private Canvas uiCanvas;

    private Slider hpSlider;

    private Slider mpSlider;

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
        preHp = hp;
        SetHpBar();
        mpSlider.value = this.mp / this.initMp;
    }

    private void SetHpBar()
    {
        uiCanvas = uiManager.uiCanvas;
        if(initMp > 0)
        {
            GameObject mpBar = Instantiate<GameObject>(mpBarPrefab, uiCanvas.transform);
            mpSlider = mpBar.GetComponent<Slider>();
            var _mpBar = mpBar.GetComponent<EnemyHpBar>();
            _mpBar.targetTr = this.gameObject.transform;
            _mpBar.offset = mpBarOffset;

        }
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpSlider = hpBar.GetComponent<Slider>();
        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;



    }
    private void OnChangeHp(int damage)
    {
        if(hp <= initHp)
        {
            this.hp -= damage;
            preHp = hp;
            hpSlider.value = this.hp / this.initHp;
            SetDamageText(damage);
            if (initMp > 0)
            {
                
                OnChangeMp(1);
            }
        }
     

    }

    public void CheckHpChange()
    {
        if(preHp != hp)
        {
            hpSlider.value = this.hp/this.initHp;
            preHp = hp;
        }

        if(hp >= initHp)
        {
            hp = initHp;
        }
    }
    public void OnChangeMp(int mana)
    {
        if(initMp > 0)
        {
            this.mp += mana;
            mpSlider.value = this.mp / this.initMp;
        }
        else if(initMp == 0)
        {
            mpSlider.value = 0;
        }
        
       
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
