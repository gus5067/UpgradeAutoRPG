using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject uiImg;
    public void OnClickYesButton()
    {
        SceneManager.LoadScene("CharacterTest");
    }

    public void OnClickNoButton()
    {
        uiImg.SetActive(false);
    }
}
