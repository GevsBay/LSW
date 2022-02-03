using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 
using UnityEngine.UI;

public class buildSlot : MonoBehaviour
{
    public TileBase tilebase;
    public Sprite image;


    public void onClick() 
    {
        mapManager.instance.selectBuildTile(tilebase, image);
    }
}
