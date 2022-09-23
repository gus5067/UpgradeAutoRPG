using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ConversationController : MonoBehaviour
{
    public static ConversationController instance;

    public Conversation curConversation;

    [SerializeField]
    private GameObject talkBackground;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI conversationText;
    [SerializeField]
    private int curConversationNum = 0;
    [SerializeField]
    private int curTalkNum = 0;
    private void Awake()
    {
        instance = this;
    }

    public void StartConversation()
    {
        if (curConversationNum == 0 && curTalkNum == 0)
        {
            talkBackground.SetActive(true);
            nameText.text = curConversation.talks[0].name;
            conversationText.text = curConversation.talks[0].talks[0];
            curTalkNum++;
            Time.timeScale = 0;
        }
        else
        {
            ConversationProgress();
        }


    }

    public void ConversationProgress()
    {
        if(curTalkNum > curConversation.talks[curConversationNum].talks.Length -1) //대화 길이 초과 경우
        {
            curTalkNum = 0;                                                         //대화 0으로 하고 상대 바꿈
            curConversationNum++;
            if(curConversationNum > curConversation.talks.Length -1)                //만약 상대가 더 없으면
            {
                curTalkNum = 0;                                                     //초기화
                curConversationNum = 0;
                talkBackground.SetActive(false);
                curConversation = null;
                Time.timeScale = 1;
            }
            else                                                                     //상대 있으면 다음 상대 대화 진행
            {
                nameText.text = curConversation.talks[curConversationNum].name;
                conversationText.text = curConversation.talks[curConversationNum].talks[curTalkNum];
                curTalkNum++;
            }
        }
        else                                                                        //대화 더 있는 경우 
        {
            nameText.text = curConversation.talks[curConversationNum].name;
            conversationText.text = curConversation.talks[curConversationNum].talks[curTalkNum];
            curTalkNum++;
        }
    }

}
