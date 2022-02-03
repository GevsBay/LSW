using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGrid : MonoBehaviour
{
    #region instancing
    public static testGrid instance; 
    #endregion

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }

    public Grid grid;

    // Start is called before the first frame update
    void Start()
    {
       grid = new Grid(60, 28, 1f);

       Invoke("loadRoadData", 1f);
    }

    void loadRoadData()
    {
        int[,] positions = grid.getXYARRAY();

        for (int x = 0; x < positions.GetLength(0); x++)
        {
            for (int y = 0; y < positions.GetLength(1); y++)
            {
                mapManager.instance.gridToTilemapPostion(x, y);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            /*
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.SetValue(mousePosition, 1); 
            */
        }
    }

}
