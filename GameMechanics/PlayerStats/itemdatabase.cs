using System.Linq;
using System.Collections.Generic; 
using UnityEngine;


public class itemdatabase : MonoBehaviour
{ 
    public item[] itemsDatabase;

    [Header("Reference Purposes")]
    public List<item> weapons;
    public List<item> clothing;
    public List<item> questItems;
     
    #region instancing
    public static itemdatabase instance;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
    #endregion
}
