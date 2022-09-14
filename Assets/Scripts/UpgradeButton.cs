using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeButton : MonoBehaviour
{
    [SerializeField]
    private Button upgradeButton;

    public void ButtonClick()
    {
        StartCoroutine(UpgradeRoutine());  
    }
    IEnumerator UpgradeRoutine()
    {
        upgradeButton.interactable = false;
        yield return new WaitForSeconds(2f);
        Upgrade();
        upgradeButton.interactable = true;
    }
    public void Upgrade()
    {
        int num = Random.Range(1, 101);

        if(num >= 5 * WeaponManager.instance.weaponValue)
        {
            Debug.Log("��ȭ ����");
            WeaponManager.instance.weaponValue++;
            WeaponManager.instance.minDamage += 2 * WeaponManager.instance.weaponValue;
            WeaponManager.instance.maxDamage += 2 * WeaponManager.instance.weaponValue;
        }
        else
        {
            Debug.Log("��ȭ ����");
            int num2 = Random.Range(1, 4);
            if(num2 >= 2)
            {
                Debug.Log("��ȭ ��ġ �϶�");
                WeaponManager.instance.minDamage -= 2 * WeaponManager.instance.weaponValue;
                WeaponManager.instance.maxDamage -= 2 * WeaponManager.instance.weaponValue;
                WeaponManager.instance.weaponValue--;
            }
            else
            {
                Debug.Log("��ȭ ��ġ ����");
             
            }
        }
    }
}
