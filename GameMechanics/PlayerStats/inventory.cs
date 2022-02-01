using System.Linq;
using System.Collections.Generic;
using UnityEngine;
 

public class inventory : MonoBehaviour
{
    #region instancing
    public static inventory instance;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
    #endregion

    public List<item> currentInventory = new List<item>();

    #region UI
    public GameObject invUi;
    public Transform contentHolder;
    public GameObject invSlot;
    #endregion

    shopManager shopmanager;
    ConversationManager conversationmanager;

    private void Start()
    {
        shopmanager = shopManager.instance;
        conversationmanager = ConversationManager.instance;

    }

    public void showInventory()
    {
        if (invUi.activeInHierarchy)
        {
            for (int i = 0; i < contentHolder.childCount; i++)
            {
                Destroy(contentHolder.GetChild(i).gameObject);
            }
            invUi.SetActive(false);
            topdMove.instance.unfreezeMovement();

        }
        else { 
          for (int i = 0; i < currentInventory.Count; i++)
          {
            inventorySlot newobj = Instantiate(invSlot, contentHolder).GetComponent<inventorySlot>();
            newobj.icon.sprite = currentInventory[i].icon;
            newobj.type = currentInventory[i].itemType;
          }
          invUi.SetActive(true);
          topdMove.instance.freezeMovement();
       }
    }

    public void sortInv(List<item> items)
    { 
            for (int i = 0; i < contentHolder.childCount; i++)
            {
                Destroy(contentHolder.GetChild(i).gameObject);
            } 
       
            for (int i = 0; i < items.Count; i++)
            {
                inventorySlot newobj = Instantiate(invSlot, contentHolder).GetComponent<inventorySlot>();
                newobj.icon.sprite = items[i].icon;
                newobj.type = items[i].itemType;
            }
            invUi.SetActive(true);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !shopmanager.uiShown && !conversationmanager.ConversationUI.activeInHierarchy) 
        {
            showInventory();
        }
    }

    public void addItem(int itemid)
    {
        currentInventory.Add(itemdatabase.instance.itemsDatabase.FirstOrDefault(x => x.itemid == itemid));
    }

    public void removeItem(int itemid)
    {
        currentInventory.Remove(currentInventory.FirstOrDefault(x => x.itemid == itemid));
    }
}
