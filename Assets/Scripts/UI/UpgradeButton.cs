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

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager= FindObjectOfType<AudioManager>();
    }

    public void PointerEnter()
    {
      audioManager.PlayerEffectSound(audioManager.audioClips[0]);
    }
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
        audioManager.PlayerEffectSound(audioManager.audioClips[1]);
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
        audioManager.PlayerEffectSound(audioManager.audioClips[1]);
        StartCoroutine(SpeicalUpgradeRoutine());
    }
    IEnumerator UpgradeRoutine()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        upgradeButton.interactable = false;
        yield return new WaitForSeconds(1f);
        weaponAnimator.SetTrigger("Upgrade");
        audioManager.PlayerEffectLoop(audioManager.audioClips[2]);
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
        yield return new WaitForSeconds(1f);
        weaponAnimator.SetTrigger("Upgrade");
        audioManager.PlayerEffectLoop(audioManager.audioClips[2]);
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
            audioManager.PlayerEffectSound(audioManager.audioClips[3]);
            WeaponManager.Instance.weaponValue++;
            WeaponManager.Instance.minDamage += (int)(WeaponManager.Instance.weaponValue * 1.2f);
            WeaponManager.Instance.maxDamage += (int)(WeaponManager.Instance.weaponValue * 1.5f);
        }
        else
        {
            audioManager.PlayerEffectSound(audioManager.audioClips[4]);
            Debug.Log("��ȭ ����");
            int num2 = Random.Range(1, 4);
            if(num2 >= 2)
            {
                Debug.Log("��ȭ ��ġ �϶�");
                WeaponManager.Instance.minDamage -= (int)(WeaponManager.Instance.weaponValue * 1.2f);
                WeaponManager.Instance.maxDamage -= (int)(WeaponManager.Instance.weaponValue * 1.5f);
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
            audioManager.PlayerEffectSound(audioManager.audioClips[3]);
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
            WeaponChanger.instance.ChangeWeaponImg();

        }
        else
        {
            audioManager.PlayerEffectSound(audioManager.audioClips[4]);
            Debug.Log("Ư�� ��ȭ ����");
            Debug.Log("���� ��ȣ : " + WeaponManager.Instance.WeaponStateNum);
        }
    }
}
