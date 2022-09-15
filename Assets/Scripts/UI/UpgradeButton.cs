using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeButton : MonoBehaviour
{
    [SerializeField]
    private Button upgradeButton;

    [SerializeField]
    private Button specialButton;

    [SerializeField]
    private Animator weaponAnimator;

    public void ButtonClick()
    {
        if(GameManager.instance.gameMoney >= WeaponManager.instance.weaponValue * 100)
        {
            GameManager.instance.gameMoney -= WeaponManager.instance.weaponValue * 100;
        }
        else
        {
            Debug.Log("돈이 부족합니다");
            return;
        }
        StartCoroutine(UpgradeRoutine());  
    }

    public void SpecialButtonClick()
    {
        if (GameManager.instance.gameGem >= 1)
        {
            GameManager.instance.gameGem--;
        }
        else
        {
            Debug.Log("보석이 부족합니다");
            return;
        }
        StartCoroutine(SpeicalUpgradeRoutine());
    }
    IEnumerator UpgradeRoutine()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        upgradeButton.interactable = false;
        weaponAnimator.SetTrigger("Upgrade");
        yield return new WaitForSeconds(2f);
        Upgrade();
        upgradeButton.interactable = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    IEnumerator SpeicalUpgradeRoutine()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        specialButton.interactable = false;
        weaponAnimator.SetTrigger("Upgrade");
        yield return new WaitForSeconds(2f);
        SpecialUpgrade();
        specialButton.interactable = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Upgrade()
    {
        int num = Random.Range(1, 101);

        if(num >= 5 * WeaponManager.instance.weaponValue)
        {
            Debug.Log("강화 성공");
            WeaponManager.instance.weaponValue++;
            WeaponManager.instance.minDamage += 2 * WeaponManager.instance.weaponValue;
            WeaponManager.instance.maxDamage += 2 * WeaponManager.instance.weaponValue;
        }
        else
        {
            Debug.Log("강화 실패");
            int num2 = Random.Range(1, 4);
            if(num2 >= 2)
            {
                Debug.Log("강화 수치 하락");
                WeaponManager.instance.minDamage -= 2 * WeaponManager.instance.weaponValue;
                WeaponManager.instance.maxDamage -= 2 * WeaponManager.instance.weaponValue;
                WeaponManager.instance.weaponValue--;
            }
            else
            {
                Debug.Log("강화 수치 유지");
             
            }
        }
    }

    public void SpecialUpgrade()
    {
        int num = Random.Range(1, 101);

        if (num >= 70)
        {
            Debug.Log("특수 강화 성공");
            
        }
        else
        {
            Debug.Log("특수 강화 실패");
        }
    }
}
