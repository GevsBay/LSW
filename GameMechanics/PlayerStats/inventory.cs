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

    inputHandler InputManager;


    private void Start()
    {  
        InputManager = inputHandler.instance;
    }

    public void showInventory()
    {
          
          for (int i = 0; i < currentInventory.Count; i++)
          {
            inventorySlot newobj = Instantiate(invSlot, contentHolder).GetComponent<inventorySlot>();
            newobj.icon.sprite = currentInventory[i].icon;
            newobj.type = currentInventory[i].itemType;
          }
          invUi.SetActive(true);
          InputManager.disableUIinteractions();
          topdMove.instance.freezeMovement();
       
    }

    void hideinventory()
    {
        for (int i = 0; i < contentHolder.childCount; i++)
        {
            Destroy(contentHolder.GetChild(i).gameObject);
        }
        invUi.SetActive(false);
        InputManager.allowUIinteractions();
        topdMove.instance.unfreezeMovement();
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
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (!InputManager.UiShown)
                showInventory();
            else if (invUi.activeInHierarchy)
            {
                hideinventory();
            }
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
