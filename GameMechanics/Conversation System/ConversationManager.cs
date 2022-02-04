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
    private bool UiSpawned;

    inputHandler InputManager;

    private void Start()
    {
        InputManager = inputHandler.instance;
    }


    private void Update()
    {
        if (ConversationUI.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
                nextLine();

            if (!dialoguestarted && canSkip && Input.GetKeyDown(KeyCode.Escape))
                skipConversations();
        }

    }

    public void greetPlayer(string greetSentance, string connotavailable, List<Conversation> conversations, conversationStarter starter)
    {
        foreach (var item in conversations)
        {
            possibleConversations.Add(item);
        }

        ConversationUI.SetActive(true);
        InputManager.disableUIinteractions();

        iconindex = starter.returnIndex(starter.charachterNpc);
        currentspeakerImage.sprite = npcIcons[iconindex];
        thisConversationstarter = starter;
        if (conversationAvailable())
        {
            StartCoroutine(scrollText(greetSentance, greetingtext));
        }
        else
        {
            StartCoroutine(scrollTextandEnd(connotavailable, greetingtext));  
        }

        greetingPanel.SetActive(true);

    }

    public void greetPlayer(string greetSentance, List<Conversation> conversations, conversationStarter starter)
    {
        foreach (var item in conversations)
        {
            possibleConversations.Add(item);
        }

        ConversationUI.SetActive(true);
        InputManager.disableUIinteractions();

        iconindex = starter.returnIndex(starter.charachterNpc);
        currentspeakerImage.sprite = npcIcons[iconindex];
        thisConversationstarter = starter;
        StartCoroutine(scrollText(greetSentance, greetingtext));

        greetingPanel.SetActive(true);
}

    void skipConversations()
    {
        possibleConversations.Clear();
        thisConversationstarter = null;
        greetingPanel.SetActive(false);
        ConversationUI.SetActive(false);
        InputManager.allowUIinteractions();
        InputManager.allowMovement();


        for (int i = 0; i < convSelectionOptionsParent.transform.childCount; i++)
        {
            Destroy(convSelectionOptionsParent.transform.GetChild(i).gameObject);
        }

        currentConversation = null;
        UiSpawned = false;
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
            if (currentConversation.Action)
            {
                topdMove.instance.unfreezeMovement();
                thisConversationstarter.onCompleteAction(currentConversation._action, currentConversation);
                finishTalking();
            } else
                endDialogue();
        }

    }

    private void endDialogue()
    {
        dialoguestarted = false;

        if (!currentConversation.repeater)
            currentConversation.finished = true;

        if (currentConversation.toUnlock.Length != 0)
        {
            foreach (var item in currentConversation.toUnlock)
            {
                item.unlocked = true;
            }
        }

        currentConversation.currentindex = 0;
          
        if(!conversationAvailable())
        {
            finishTalking();
            return;
        }    
        dialogueUi.SetActive(false);
        greetingPanel.SetActive(true);
        StartCoroutine(scrollText(thisConversationstarter.wantmore, greetingtext));
    }

    bool conversationAvailable()
    {
       for (int i = 0; i<possibleConversations.Count; i++)
        {
            if (!possibleConversations[i].finished && possibleConversations[i].unlocked)
            {
                return true; 
            }
        }
        return false;

    }

    public void finishTalking()
    {
        possibleConversations.Clear();
        thisConversationstarter = null;
        dialoguestarted = false;
        currentConversation.currentindex = 0;
        for (int i = 0; i < convSelectionOptionsParent.transform.childCount; i++)
        {
            Destroy(convSelectionOptionsParent.transform.GetChild(i).gameObject);
        }
        greetingPanel.SetActive(false);
        dialogueUi.SetActive(false);
        ConversationUI.SetActive(false);
        InputManager.allowUIinteractions();
        InputManager.allowMovement();


        currentConversation = null;
        UiSpawned = false;
    }

    void updateDialogueUi()
    {
        currentspeakerImage.sprite = currentConversation.dialogues[currentConversation.currentindex].orator == dialogue.speaker.player ? playerIcon : npcIcons[iconindex];
        StartCoroutine(scrollText(currentConversation.dialogues[currentConversation.currentindex].speechContent, Content));
    }

    IEnumerator scrollText(string sentance, Text target)
    {
        //only play if npc talks
        if (!dialoguestarted || currentConversation.dialogues[currentConversation.currentindex].orator == dialogue.speaker.npc)
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

        if (!UiSpawned || !dialoguestarted)
        {
            spawnUI();
        }
        canSkip = true;
        scrollFinished = true;
        thisConversationstarter.stopTalkAnimation(); 
         
    }

    IEnumerator scrollTextandEnd(string sentance, Text target)
    {
        //only play if npc talks
        if (!dialoguestarted || currentConversation.dialogues[currentConversation.currentindex].orator == dialogue.speaker.npc)
            thisConversationstarter.playTalkAnimation();

        canSkip = false;
        scrollFinished = true;
        currentSentance = sentance;
        currentText = target;

        target.text = "";
        foreach (char c in sentance)
        {
            target.text += c;

            yield return new WaitForSecondsRealtime(0.05f);
        } 
        canSkip = true; 
        thisConversationstarter.stopTalkAnimation();

        Invoke("closeUi", 0.4f);
    }

    void closeUi()
    {
        possibleConversations.Clear();
        thisConversationstarter = null;
        dialoguestarted = false;
        dialogueUi.SetActive(false);
        ConversationUI.SetActive(false);
        InputManager.allowUIinteractions();
        InputManager.allowMovement();
        UiSpawned = false;
    }

    private void endScrollFast()
    {
        StopAllCoroutines();
        thisConversationstarter.stopTalkAnimation(); 
        currentText.text = currentSentance;

        if (!UiSpawned || !dialoguestarted)
        {
            spawnUI();
        }

        canSkip = true;
        scrollFinished = true; 
    }

    void skipDelay()
    {
        skipOnDelay = false;
    }

    void spawnUI() 
    {
        for (int i = 0; i < possibleConversations.Count; i++)
        {
            if (!possibleConversations[i].finished && possibleConversations[i].unlocked)
            {
                GameObject newobj = Instantiate(dialogueSelectionPrefab, convSelectionOptionsParent.transform);
                newobj.GetComponent<conversationSelector>().conversation = possibleConversations[i];
                newobj.GetComponent<Text>().text = possibleConversations[i].shortDescription;
            }  
        }
        UiSpawned = true; 
    }

}
