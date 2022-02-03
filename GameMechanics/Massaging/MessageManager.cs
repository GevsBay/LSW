using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassageManager : MonoBehaviour
{
    #region instance
    public static MassageManager instance;

    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
    }

    #endregion

    public string error_roadbuild;

    public void popout(string message)
    {
    
    }
}
