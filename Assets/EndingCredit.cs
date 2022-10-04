using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EndingCredit : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private void OnEnable()
    {
        this.transform.SetAsLastSibling();
        StartCoroutine(EndingRoutine());    
    }

    IEnumerator EndingRoutine()
    {
        foreach(KeyValuePair<string, int> data in GameManager.Instance.gameCount)
        {
            text.text = data.Key +" : " + data.Value;
            yield return new WaitForSeconds(2f);
        }
        text.text = "플레이해주셔서 감사합니다.";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("UpgradeTest");
    }
}
