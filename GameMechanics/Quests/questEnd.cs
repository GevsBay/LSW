using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questEnd : MonoBehaviour
{
    questManager questManager;

    private void Start()
    {
        questManager = questManager.instance;
    }
    public void endQuest()
    {   
            if (questManager.activeQuest != null)
            {
                questManager.endQuest();
            } 
    } 
}
