using System.Linq;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;


public class Sorting : MonoBehaviour
{
    itemdatabase database;
    [SerializeField]
    item.type param;
    private void Start()
    {
        database = itemdatabase.instance;
    }

    public void Sort()
    {
        List<item> res = new List<item>();
        foreach (var entry in database.itemsDatabase)
        {
            if(entry.itemType == param)
            {
                res.Add(entry);
            }

        }

        shopManager.instance.Sort(res);
    }

    public void SortSelectionMenu()
    {
        List<item> res = new List<item>();
        foreach (var entry in inventory.instance.currentInventory)
        {
            if (entry.itemType == param)
            {
                res.Add(entry);
            }

        }

        inventory.instance.sortInv(res);
    }

    public void spriteMenuSelect()
    {
        Sort();
    }

    public void SwitchMode()
    {
        shopManager.instance.SwitchMode(); 
    }
}
