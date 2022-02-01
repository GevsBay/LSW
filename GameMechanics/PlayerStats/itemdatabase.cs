using System.Linq; 
using UnityEngine;


public class itemdatabase : MonoBehaviour
{ 
    public item[] itemsDatabase;

    #region instancing
    public static itemdatabase instance;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
    #endregion
}
