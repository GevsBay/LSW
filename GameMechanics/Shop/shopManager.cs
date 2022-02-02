using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class shopManager : MonoBehaviour
{
    #region instancing
    public static shopManager instance;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
    #endregion

    #region UI
    public GameObject panel;
    public Image icon; 

    public Text description;
    public Text money; 
    public Button buy;


    public int price; 
    public int id;

    public Image ModeSwitcher;
    public Sprite inventoryIcon;
    public Sprite shopIcon;
    bool buying;


    public GameObject shopui;
    public Text shopButtonText;

    public Transform contentHolder; 

    public GameObject saleItem; 
    [SerializeField] public bool uiShown;
    #endregion
     
    itemdatabase itemDatabase;
    topdMove movement;
    playerStats playerstats;
    inventory _inventory;

    private void Start()
    {
        itemDatabase = itemdatabase.instance;
        playerstats = playerStats.instance;
        movement = topdMove.instance;

        _inventory = inventory.instance;

    }

    public void highlightPurchase(saleItem item)
    {
        icon.sprite = item.icon.sprite;
        description.text = item.name.text + " Price $" + item.pricetext.text; 
        buy.interactable = true;  
        id = item.id;
        price = item.price;
        panel.SetActive(true);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && shopui.activeInHierarchy)
            disableUI();
    }

    public void GenerateShopFront(Conversation.action _action)
    {
        if(_action == Conversation.action.buy) {
            int count = itemDatabase.itemsDatabase.Length;

            for (int i = 0; i < count; i++)
            {
                if (itemDatabase.itemsDatabase[i].itemType == item.type.questitem)
                    continue;

                saleItem newitem = Instantiate(saleItem, contentHolder).GetComponent<saleItem>();
                newitem.icon.sprite = itemDatabase.itemsDatabase[i].icon;
                newitem.name.text = itemDatabase.itemsDatabase[i].description;
                newitem.pricetext.text = itemDatabase.itemsDatabase[i].price.ToString();
                newitem.id = itemDatabase.itemsDatabase[i].itemid;
                newitem.price = itemDatabase.itemsDatabase[i].price;
            } 
            buying = true;
            shopButtonText.text = "Purchase";
            ModeSwitcher.sprite = inventoryIcon;

        }
        else
        {
            int count = _inventory.currentInventory.Count;

            for (int i = 0; i < count; i++)
            {
                if (_inventory.currentInventory[i].itemType == item.type.questitem)
                    continue;

                saleItem newitem = Instantiate(saleItem, contentHolder).GetComponent<saleItem>();
                newitem.icon.sprite = _inventory.currentInventory[i].icon;
                newitem.name.text = _inventory.currentInventory[i].description;
                newitem.pricetext.text = _inventory.currentInventory[i].price.ToString();
                newitem.id = _inventory.currentInventory[i].itemid;
                newitem.price = _inventory.currentInventory[i].price; 
            } 
            ModeSwitcher.sprite = shopIcon;
            buying = false;
            shopButtonText.text = "Sell"; 

        }

        movement.freezeMovement();
        money.text = playerstats.Money.ToString();
        shopui.SetActive(true); 
        uiShown = true;
    }

    private void disableUI()
    {
        for (int i = 0; i < contentHolder.childCount; i++)
        {
            Destroy(contentHolder.GetChild(i).gameObject);
        }

        movement.unfreezeMovement();

        shopui.SetActive(false);

        uiShown = false;

    }

    public void SwitchMode()
    {
        for (int i = 0; i < contentHolder.childCount; i++)
        {
            Destroy(contentHolder.GetChild(i).gameObject);
        }

        if (buying)
        { 
            int count = _inventory.currentInventory.Count;

            for (int i = 0; i < count; i++)
            {
                if (_inventory.currentInventory[i].itemType == item.type.questitem)
                    continue;

                saleItem newitem = Instantiate(saleItem, contentHolder).GetComponent<saleItem>();
                newitem.icon.sprite = _inventory.currentInventory[i].icon;
                newitem.name.text = _inventory.currentInventory[i].description;
                newitem.pricetext.text = _inventory.currentInventory[i].price.ToString();
                newitem.id = _inventory.currentInventory[i].itemid;
                newitem.price = _inventory.currentInventory[i].price;
            } 
            buying = false;
            shopButtonText.text = "Sell";

            ModeSwitcher.sprite = shopIcon;
        }
        else
        {
            int count = itemDatabase.itemsDatabase.Length;

            for (int i = 0; i < count; i++)
            {
                if (itemDatabase.itemsDatabase[i].itemType == item.type.questitem)
                    continue;

                saleItem newitem = Instantiate(saleItem, contentHolder).GetComponent<saleItem>();
                newitem.icon.sprite = itemDatabase.itemsDatabase[i].icon;
                newitem.name.text = itemDatabase.itemsDatabase[i].description;
                newitem.pricetext.text = itemDatabase.itemsDatabase[i].price.ToString();
                newitem.id = itemDatabase.itemsDatabase[i].itemid;
                newitem.price = itemDatabase.itemsDatabase[i].price;
            } 

            buying = true;
            shopButtonText.text = "Purchase";

            ModeSwitcher.sprite = inventoryIcon;
        }
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < contentHolder.childCount; i++)
        {
            Destroy(contentHolder.GetChild(i).gameObject);
        }
        int count = _inventory.currentInventory.Count;

        for (int i = 0; i < count; i++)
        {
            saleItem newitem = Instantiate(saleItem, contentHolder).GetComponent<saleItem>();
            newitem.icon.sprite = _inventory.currentInventory[i].icon;
            newitem.name.text = _inventory.currentInventory[i].description;
            newitem.pricetext.text = _inventory.currentInventory[i].price.ToString();
            newitem.id = _inventory.currentInventory[i].itemid;
            newitem.price = _inventory.currentInventory[i].price;
        }

    }

    public void Sort(List<item> items)
    {
        for (int i = 0; i < contentHolder.childCount; i++)
        {
            Destroy(contentHolder.GetChild(i).gameObject);
        }

        int count = items.Count;

        for (int i = 0; i < count; i++)
        {
            if (_inventory.currentInventory[i].itemType == item.type.questitem)
                continue;

            saleItem newitem = Instantiate(saleItem, contentHolder).GetComponent<saleItem>();
            newitem.icon.sprite = items[i].icon;
            newitem.name.text = items[i].description;
            newitem.pricetext.text = items[i].price.ToString();
            newitem.id = items[i].itemid;
            newitem.price = items[i].price;

        }
         
    }

    public void processButtonClick()
    {
        if (buying)
            Purchase();
        else
            Sell();
        
    }


    public void Purchase() 
    {
        playerstats.Money -= price;
        money.text = playerstats.Money.ToString();
        buy.interactable = false;
        inventory.instance.addItem(id);
    }

    public void Sell()
    {
        playerstats.Money += price;
        money.text = playerstats.Money.ToString();
        buy.interactable = false;
        inventory.instance.removeItem(id);

        UpdateInventoryUI();
    }

}
