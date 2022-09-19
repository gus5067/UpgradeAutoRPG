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
        if(GameManager.Instance.gameMoney >= WeaponManager.Instance.weaponValue * 100)
        {
            GameManager.Instance.gameMoney -= WeaponManager.Instance.weaponValue * 100;
        }
        else
        {
            Debug.Log("���� �����մϴ�");
            return;
        }
        StartCoroutine(UpgradeRoutine());  
    }

    public void SpecialButtonClick()
    {
        if (GameManager.Instance.gameGem >= 1)
        {
            GameManager.Instance.gameGem--;
        }
        else
        {
            Debug.Log("������ �����մϴ�");
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

        if(num >= 5 * WeaponManager.Instance.weaponValue)
        {
            Debug.Log("��ȭ ����");
            WeaponManager.Instance.weaponValue++;
            WeaponManager.Instance.minDamage += 4 * WeaponManager.Instance.weaponValue;
            WeaponManager.Instance.maxDamage += 4 * WeaponManager.Instance.weaponValue;
        }
        else
        {
            Debug.Log("��ȭ ����");
            int num2 = Random.Range(1, 4);
            if(num2 >= 2)
            {
                Debug.Log("��ȭ ��ġ �϶�");
                WeaponManager.Instance.minDamage -= 4 * WeaponManager.Instance.weaponValue;
                WeaponManager.Instance.maxDamage -= 4 * WeaponManager.Instance.weaponValue;
                WeaponManager.Instance.weaponValue--;
            }
            else
            {
                Debug.Log("��ȭ ��ġ ����");
             
            }
        }
    }

    public void SpecialUpgrade()
    {
        int num = Random.Range(1, 101);

        if (num >= 49)
        {
            Debug.Log("Ư�� ��ȭ ����");
            WeaponManager.Instance.WeaponStateNum = Random.Range(1, 3);
            switch (WeaponManager.Instance.WeaponStateNum)
            {
                case 0:
                    WeaponManager.Instance.swordName = "�⺻ ������ ��";
        
                    break;
                case 1:
                    WeaponManager.Instance.swordName = "������ ��";
       
                    break;
                case 2:
                    WeaponManager.Instance.swordName = "������ ������ ��";
                    break;

            }
            Debug.Log("���� ��ȣ : " + WeaponManager.Instance.WeaponStateNum);

        }
        else
        {
            Debug.Log("Ư�� ��ȭ ����");
            WeaponManager.Instance.WeaponStateNum = 0;
            Debug.Log("���� ��ȣ : " + WeaponManager.Instance.WeaponStateNum);
        }
    }
}
