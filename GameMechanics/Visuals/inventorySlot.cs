using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class inventorySlot : MonoBehaviour
{
    public Image icon;
    public item.type type;
      
    public void onClicked()
    {
        int index = Array.IndexOf(Enum.GetValues(type.GetType()), type);  
        playerStats.instance.swapGraphics(icon, index);
    }
}
