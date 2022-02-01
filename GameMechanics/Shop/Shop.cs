using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : conversationStarter
{

    shopManager manager;
     
    private void Start()
    {
        manager = shopManager.instance;
    }

    public override void onCompleteAction(Conversation.action action) 
    {
        GenerateShopFront(action);
    }

    private void GenerateShopFront(Conversation.action action)
    {
        manager.GenerateShopFront(action);
    }

    
}
