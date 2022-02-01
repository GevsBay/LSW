using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[RequireComponent(typeof(CircleCollider2D))]
public class conversationStarter : MonoBehaviour
{
    public string defaultLine = "Hi Stranger, is there anything that I can do to help You";
    public List<Conversation> possibleConversations = new List<Conversation>();
    public float radius = 2f;


    public enum npcs { seller, jack };
    public npcs charachterNpc;

    public int returnIndex(npcs type)
    {
        int index = Array.IndexOf(Enum.GetValues(type.GetType()), type);
        return index;
    }

    #region animations
    public string talkanimationTrigger = "talking";

    public Animator animator;
    #endregion

    public void playTalkAnimation()
    {
        if(animator != null)
        { 
            animator.SetBool(talkanimationTrigger, true);  
        }
    }

    public void stopTalkAnimation()
    {
        if (animator != null)
        {
           animator.SetBool(talkanimationTrigger, false); 
        }
    }

    private void Awake()
    {
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = radius; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            ConversationManager.instance.greetPlayer(defaultLine, possibleConversations, this);
            topdMove.instance.freezeMovement();  
        }
    }

    public virtual void onCompleteAction(Conversation.action action)
    {
        
    }
}
