using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ConversationManager : MonoBehaviour
{
    #region instancing
    public static ConversationManager instance;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
    #endregion

    #region Visuals/Ui
    [Header("UI")]
    public GameObject ConversationUI;

    public GameObject dialogueUi;
    public Image currentspeakerImage;
    public Sprite playerIcon;
    public Sprite[] npcIcons;
    int iconindex;

    public Text Content;

    [Header("GreetingPanel")] 
    public Text greetingtext;
    public GameObject greetingPanel;
    public GameObject convSelectionOptionsParent;
    public GameObject dialogueSelectionPrefab;
    #endregion

    #region TextScroller  
    bool canSkip = false;
    [SerializeField]
    bool skipOnDelay = false; 
    bool scrollFinished = false;
    bool dialoguestarted = false;
    string currentSentance;
    Text currentText;
    #endregion

    public Conversation currentConversation; 
    public List<Conversation> possibleConversations = new List<Conversation>();
    conversationStarter thisConversationstarter;

    private void Update()
    {
        if (ConversationUI.activeInHierarchy)
        { 
            if(Input.GetMouseButtonDown(0))
            nextLine();

            if (!dialoguestarted && canSkip && Input.GetKeyDown(KeyCode.Escape))
                skipConversations();
        } 

    }

    public void greetPlayer(string greetSentance, List<Conversation> conversations, conversationStarter starter)
    {
        foreach (var item in conversations)
        {
            possibleConversations.Add(item); 
        }

        ConversationUI.SetActive(true);

        iconindex = starter.returnIndex(starter.charachterNpc);
        currentspeakerImage.sprite = npcIcons[iconindex];
        thisConversationstarter = starter;
        StartCoroutine(scrollText(greetSentance, greetingtext));

        greetingPanel.SetActive(true);

    }

    void skipConversations() 
    {
        possibleConversations.Clear();
        this.thisConversationstarter = null;
        greetingPanel.SetActive(false);
        ConversationUI.SetActive(false);

        for (int i = 0; i < convSelectionOptionsParent.transform.childCount; i++)
        {
            Destroy(convSelectionOptionsParent.transform.GetChild(i).gameObject);
        }
        topdMove.instance.unfreezeMovement();
        currentConversation = null;

    }


    public void startDialogue(Conversation conversation)
    {
        skipOnDelay = true;
        Invoke("skipDelay", 0.3f);

        dialoguestarted = true;

        greetingPanel.SetActive(false);
        dialogueUi.SetActive(true); 
        currentConversation = conversation;

        updateDialogueUi();
    }

    public void nextLine()
    {
        if (scrollFinished == false && !skipOnDelay)
        {
            endScrollFast();
            return; 
        }

        if (!canSkip || !dialoguestarted)
            return;

        if (!currentConversation.dialogues[currentConversation.currentindex].lastLine)
        {
            currentConversation.currentindex++;
            updateDialogueUi();
        }
        else
        {
            if(currentConversation.Action)
            thisConversationstarter.onCompleteAction(currentConversation._action);

            endDialogue();
        }
       
    }
     
    private void endDialogue()
    {
        possibleConversations.Clear();
        this.thisConversationstarter = null;
        dialoguestarted = false; 
        greetingPanel.SetActive(false);
        dialogueUi.SetActive(false);
        ConversationUI.SetActive(false);
        currentConversation.currentindex = 0;
         
        for (int i = 0; i < convSelectionOptionsParent.transform.childCount; i++)
        {
            Destroy(convSelectionOptionsParent.transform.GetChild(i).gameObject);
        }

        topdMove.instance.unfreezeMovement();
        currentConversation = null; 
    }

    void updateDialogueUi()
    {
        currentspeakerImage.sprite = currentConversation.dialogues[currentConversation.currentindex].orator == dialogue.speaker.player ? playerIcon : npcIcons[iconindex];
        StartCoroutine(scrollText(currentConversation.dialogues[currentConversation.currentindex].speechContent, Content)); 
    }

    IEnumerator scrollText(string sentance, Text target)
    {
        //only play if npc talks
        if(!dialoguestarted || currentConversation.dialogues[currentConversation.currentindex].orator == dialogue.speaker.npc)
        thisConversationstarter.playTalkAnimation();
        
        canSkip = false;
        scrollFinished = false;
        currentSentance = sentance;
        currentText = target;

        target.text = "";
        foreach (char c in sentance)
        {
            target.text += c;

            yield return new WaitForSecondsRealtime(0.05f);
        }

        canSkip = true;
        scrollFinished = true;
        thisConversationstarter.stopTalkAnimation(); 

        if (!dialoguestarted)
        {
            spawnUI(); 
        }
         
    }

    void skipDelay()
    {
        skipOnDelay = false;  
    }


    private void endScrollFast()
    {
        StopAllCoroutines();
        thisConversationstarter.stopTalkAnimation(); 
        currentText.text = currentSentance;
        canSkip = true;
        scrollFinished = true;

        if (!dialoguestarted)
        {
            spawnUI();
        }
    }


    void spawnUI() 
    {
        for (int i = 0; i < possibleConversations.Count; i++)
        {
            GameObject newobj = Instantiate(dialogueSelectionPrefab, convSelectionOptionsParent.transform);
            newobj.GetComponent<conversationSelector>().conversation = possibleConversations[i];
            newobj.GetComponent<Text>().text = possibleConversations[i].shortDescription;
        }
    }
  
}
