using System.Linq; 
using UnityEngine;
using UnityEngine.UI;


public class questManager : MonoBehaviour
{
    #region instancing
    public static questManager instance;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
    #endregion

    #region Visuals/Ui
    [Header("UI")]
    public GameObject questUI;
    public Text currentQuestName;
    #endregion

    inventory inventory;
    playerStats stats;

    public quest activeQuest;
    public questStarter currentQuestStarter;


    private void Start()
    {
        inventory = inventory.instance;
        stats = playerStats.instance;
    }

    public void startQuest(quest quest, questStarter starter)
    {
        activeQuest = quest;
        currentQuestStarter = starter;
        for (int i = 0; i < quest.questStartObjects.Length; i++)
        {
            inventory.addItem(quest.questStartObjects[i]);
        }
        updateUI();
    }

    private void updateUI()
    {
        questUI.SetActive(true);
        currentQuestName.text = activeQuest.questDescription;
    }

    public void endQuest() 
    {
        for (int i = 0; i < activeQuest.lostObjects.Length; i++)
        {
            inventory.removeItem(activeQuest.lostObjects[i]);
        }

        for (int i = 0; i < activeQuest.rewardObjects.Length; i++)
        {
            inventory.addItem(activeQuest.rewardObjects[i]);
        }

        stats.Money += activeQuest.moneyRewarded;
        stats.Money -= activeQuest.MoneyDeducted;


        questUI.SetActive(false);
        currentQuestName.text = "";

        activeQuest.questCompleted = true;
        activeQuest = null;
    }

    public bool questResourcesGathered() 
    {
        for (int i = 0; i < activeQuest.lostObjects.Length; i++)
        {
            var obj = inventory.currentInventory.Any(n => n.itemid == activeQuest.lostObjects[i]);
            if (!obj)
                return false;
        }
        if(stats.Money < activeQuest.MoneyDeducted)
            return false;

        return true;
    }
     

    void Update()
    {
       
    }
}
