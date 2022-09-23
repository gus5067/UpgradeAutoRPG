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
        if(curTalkNum > curConversation.talks[curConversationNum].talks.Length -1) //��ȭ ���� �ʰ� ���
        {
            curTalkNum = 0;                                                         //��ȭ 0���� �ϰ� ��� �ٲ�
            curConversationNum++;
            if(curConversationNum > curConversation.talks.Length -1)                //���� ��밡 �� ������
            {
                curTalkNum = 0;                                                     //�ʱ�ȭ
                curConversationNum = 0;
                talkBackground.SetActive(false);
                curConversation = null;
                Time.timeScale = 1;
            }
            else                                                                     //��� ������ ���� ��� ��ȭ ����
            {
                nameText.text = curConversation.talks[curConversationNum].name;
                conversationText.text = curConversation.talks[curConversationNum].talks[curTalkNum];
                curTalkNum++;
            }
        }
        else                                                                        //��ȭ �� �ִ� ��� 
        {
            nameText.text = curConversation.talks[curConversationNum].name;
            conversationText.text = curConversation.talks[curConversationNum].talks[curTalkNum];
            curTalkNum++;
        }
    }

}
