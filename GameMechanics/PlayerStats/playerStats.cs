using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct item
{
    public int itemid;
    public Sprite icon;
    public string description;
    public int price;

    public enum type { hood, face, elbow, leg, pelvis, shoulder, torso, wrist, boot, weapon, questitem}
    public type itemType;
}

[System.Serializable]
public struct spawnItem
{
    public int itemId;
    public GameObject prefab;
}


[System.Serializable]
public struct equipslot
{
    public enum type { hood, face, elbow, leg, pelvis, shoulder, torso, wrist, boot }
    public type itemType; 
    public SpriteRenderer image;
}
  
public class playerStats : MonoBehaviour
{

    #region instancing
    public static playerStats instance;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
    #endregion

    public int health = 100;
    public int Money = 1500;

    public equipslot[] equipslots;

    public void swapGraphics(Image icon, int index) 
    {
        equipslots[index].image.sprite = icon.sprite;
    }
  
}
