using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class mapManager : MonoBehaviour
{
    #region instancing
    public static mapManager instance;
     
    #endregion


    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private List<tileData> tileDatas;

    public TileBase currentBuildTile;

    public Dictionary<TileBase, tileData> dataFromTiles;

    inputHandler InputManager;

    testGrid testgrid;



    #region Building
    public bool building;
    public KeyCode buildKey;
    #endregion

    #region BuildUI
     public GameObject buildUi;
     public Transform content;
     public GameObject buildSlot;
     public Image inspectImage; 
     #endregion


    private void Awake()
    {
        if (instance != this)
            instance = this;


        dataFromTiles = new Dictionary<TileBase, tileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData); 
            }
        }
    }

    private void Start()
    {
        InputManager = inputHandler.instance;
        testgrid = testGrid.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(buildKey))
        { 

            building = !building;

            if (building)
                OpenBuildUi();
            else
                CloseBuildUi();
        }

        if (building)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(!EventSystem.current.IsPointerOverGameObject())
                   BuildingaTile();
            }
        }
        
    }

    private void CloseBuildUi()
    {
        InputManager.allowMovement();
        InputManager.allowUIinteractions(); 
        buildUi.SetActive(false);
    }

    private void OpenBuildUi()
    {
        InputManager.disableMovement();
        InputManager.disableUIinteractions();
        buildUi.SetActive(true);
    }


    public void selectBuildTile(TileBase tile, Sprite img) 
    {
        currentBuildTile = tile;
        inspectImage.sprite = img;
    }

    void BuildingaTile() 
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        Vector3 cellPosition = map.CellToWorld(gridPosition);

        TileBase clickedTile = map.GetTile(gridPosition);

        if (testgrid.grid.GetValue((int)cellPosition.x, (int)cellPosition.y) == 1) 
        {
            Debug.Log(cellPosition);
            MessageManager.instance.popout(MessageManager.instance.error_roadbuild);
            return;
        }
        else //buil Somethiung
            map.SetTile(gridPosition, currentBuildTile);

        /*
        if (dataFromTiles[clickedTile] == null)
        {
            MessageManager.instance.popout(MessageManager.instance.error_roadbuild);
            return;
        }

        if (dataFromTiles[clickedTile].TileType == tileData.tileType.road)
        {
            MessageManager.instance.popout(MessageManager.instance.error_roadbuild);
        }
        else
            map.SetTile(gridPosition, currentBuildTile);
        */
    }

    public void gridToTilemapPostion(int x, int y) 
    { 
        Vector3Int gridPosition = map.WorldToCell(new Vector2(x, y)); 
        TileBase clickedTile = map.GetTile(gridPosition);
        if(clickedTile != null && dataFromTiles[clickedTile] != null)
        {
            if(dataFromTiles[clickedTile].TileType == tileData.tileType.road)
            {
               
                testgrid.grid.SetValue(map.CellToWorld(gridPosition), 1);
                //map.SetTile(gridPosition, currentBuildTile); 
            }
        }
    } 

    public tileData GetTileData(Vector3Int tilePosition)
    {
        TileBase tile = map.GetTile(tilePosition);

        if (tile == null)
            return null;
        else
            return dataFromTiles[tile];
    } 


}
