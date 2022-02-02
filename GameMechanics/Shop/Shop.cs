using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : conversationStarter
{

    shopManager manager;

    public override void Start()
    {
        base.Start();
        manager = shopManager.instance; 
    } 

    public override void onCompleteAction(Conversation.action action, Conversation conv) 
    {
        GenerateShopFront(action);
    }

    private void GenerateShopFront(Conversation.action action)
    {
        manager.GenerateShopFront(action);
    }

    
}
