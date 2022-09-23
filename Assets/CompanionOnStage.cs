using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CompanionOnStage : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Conversation _conversation;
    public Conversation conversation { get { return _conversation; } }
    
    public void Interact()
    {
      
            StageMapController.Instance.isPlayerTrigger[0] = true;
            ConversationController.instance.curConversation = conversation;
            ConversationController.instance.StartConversation();
       
      
    }
}
