using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conversationSelector : MonoBehaviour 
{
    public Conversation conversation;

    public void onClick()
    {
        ConversationManager.instance.startDialogue(conversation);
        gameObject.SetActive(false);
    }

    public void next()
    {
        ConversationManager.instance.nextLine();
    }
     
}
