using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questStarter : MonoBehaviour
{
    public bool inProgress;
    questManager questManager;

    private void Start()
    {
        questManager = questManager.instance;
    }
    public void startQuest(quest quest) 
    {
        inProgress = true;

        if (quest != null)
        {
            if (questManager.activeQuest == null)
            {
                questManager.startQuest(quest, this);
            }
        }
    }

}
