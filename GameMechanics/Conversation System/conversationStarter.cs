using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[RequireComponent(typeof(CircleCollider2D))]
public class conversationStarter : MonoBehaviour
{
    public string defaultLine = "Hi Stranger, is there anything that I can do to help You?";
    public string noConversationAvailable = "Sorry, don't have much to say now!";
    public string wantmore = "Anything Else?";
      
    public List<Conversation> possibleConversations = new List<Conversation>();
    public float radius = 2f;

    questStarter questStarter;
    questManager questManager;

    public KeyCode interactionKey = KeyCode.F;
    public GameObject alertObj;
    public Transform alertParent;
    GameObject alertSpawned;
    public bool canStart = false;
    inputHandler InputManager;


    public enum npcs { seller, jack, crazykyle };
    public npcs charachterNpc; 

    private void Awake()
    {
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = radius;
    }

     
    public virtual void Start()
    {
        InputManager = inputHandler.instance; 
        questStarter = GetComponent<questStarter>();
        questManager = questManager.instance;
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(interactionKey) && canStart && !InputManager.UiShown)
        {
            if (questManager.activeQuest == null)
            {
                ConversationManager.instance.greetPlayer(defaultLine, noConversationAvailable, possibleConversations, this);
                InputManager.disableMovement(); 
            }
            else 
            {
                if (questStarter != null) { 
                    if (questManager.currentQuestStarter == questStarter)
                    {
                        if (!questManager.questResourcesGathered())
                        {
                            List<Conversation> emprty = new List<Conversation>();
                            ConversationManager.instance.greetPlayer(questManager.activeQuest.defaultInProgressLine, emprty, this); 
                        }
                        else
                            ConversationManager.instance.greetPlayer(questManager.activeQuest.SuccessLine, questManager.activeQuest.relatedconversations, this);

                        InputManager.disableMovement(); 
                    }
                }
                else
                {
                    ConversationManager.instance.greetPlayer(defaultLine, noConversationAvailable, possibleConversations, this);
                    InputManager.disableMovement();
                }
            }
        }
    }
     
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

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
             canStart = true;
             AlertPlayer(); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(alertSpawned);
            canStart = false;
        }
    }

    public virtual void AlertPlayer()
    {
        alertSpawned = Instantiate(alertObj, alertParent.position, Quaternion.identity);
    }


    public virtual void onCompleteAction(Conversation.action action, Conversation conv)
    {
        if (action == Conversation.action.quest)
        {
            questStarter.startQuest(conv.quest);
        }
        else if (action == Conversation.action.questEnd) 
        {
            questManager.endQuest();
        }
    }
}
