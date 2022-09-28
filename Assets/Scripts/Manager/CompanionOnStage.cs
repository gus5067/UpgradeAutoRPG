using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CompanionOnStage : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int num;
    [SerializeField]
    private Conversation _conversation;
    public Conversation Conversation { get { return _conversation; } }

    [SerializeField]
    private Conversation _denyConversation;

    public Conversation DenyConversation { get { return _denyConversation; } }
    
    public void Interact()
    {
      
        if(StageMapController.Instance.questTrigger[num] != true)
        {
            ConversationController.instance.curConversation = DenyConversation;
            ConversationController.instance.StartConversation();
        }
        else
        {
            StageMapController.Instance.isPlayerTrigger[num] = true;
            ConversationController.instance.curConversation = Conversation;
            ConversationController.instance.StartConversation();
        }
           
    }
}
