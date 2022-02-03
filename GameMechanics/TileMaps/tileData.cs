using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu] 
public class tileData : ScriptableObject
{
    public TileBase[] tiles;

    public float ad = 1;

    public enum tileType { road, grass, gravel}
    public tileType TileType;

}
