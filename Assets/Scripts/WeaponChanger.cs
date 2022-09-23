using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChanger : MonoBehaviour
{
    public static WeaponChanger instance;

    private void Awake()
    {
        instance = this;
    }
    [SerializeField]
    private Image weaponImg;

    [SerializeField]
    private Sprite[] sprites;

    private void Start()
    {
        ChangeWeaponImg();
    }

    public void ChangeWeaponImg()
    {
        switch (WeaponManager.Instance.WeaponStateNum)
        {
            case 0:
                weaponImg.sprite = sprites[0];
                break;
            case 1:
                weaponImg.sprite = sprites[1];
                break;
            case 2:
                weaponImg.sprite = sprites[2];
                break;
        }
    }
}
