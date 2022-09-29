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
            Debug.Log("돈이 부족합니다");
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
            Debug.Log("보석이 부족합니다");
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
            Debug.Log("강화 성공");
            audioManager.PlayerEffectSound(audioManager.audioClips[3]);
            WeaponManager.Instance.weaponValue++;
            WeaponManager.Instance.minDamage += (int)(WeaponManager.Instance.weaponValue * 1.2f);
            WeaponManager.Instance.maxDamage += (int)(WeaponManager.Instance.weaponValue * 1.5f);
        }
        else
        {
            audioManager.PlayerEffectSound(audioManager.audioClips[4]);
            Debug.Log("강화 실패");
            int num2 = Random.Range(1, 4);
            if(num2 >= 2)
            {
                Debug.Log("강화 수치 하락");
                WeaponManager.Instance.minDamage -= (int)(WeaponManager.Instance.weaponValue * 1.2f);
                WeaponManager.Instance.maxDamage -= (int)(WeaponManager.Instance.weaponValue * 1.5f);
                WeaponManager.Instance.weaponValue--;
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

        if (num >= 49)
        {
            Debug.Log("특수 강화 성공");
            audioManager.PlayerEffectSound(audioManager.audioClips[3]);
            WeaponManager.Instance.WeaponStateNum = Random.Range(1, 3);
            switch (WeaponManager.Instance.WeaponStateNum)
            {
                case 0:
                    WeaponManager.Instance.swordName = "기본 전사의 검";
        
                    break;
                case 1:
                    WeaponManager.Instance.swordName = "흡혈의 검";
       
                    break;
                case 2:
                    WeaponManager.Instance.swordName = "강력한 전사의 검";
                    break;

            }
            Debug.Log("무기 번호 : " + WeaponManager.Instance.WeaponStateNum);
            WeaponChanger.instance.ChangeWeaponImg();

        }
        else
        {
            audioManager.PlayerEffectSound(audioManager.audioClips[4]);
            Debug.Log("특수 강화 실패");
            Debug.Log("무기 번호 : " + WeaponManager.Instance.WeaponStateNum);
        }
    }
}
