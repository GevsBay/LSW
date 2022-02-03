using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputHandler : MonoBehaviour
{
    #region instancing
    public static inputHandler instance;

    private void Awake()
    {
        if (instance != this)
            instance = this;
    }
    #endregion

    public bool movementDisabled = false;
    public bool UiShown = false;

    topdMove playerMovement; 

    private void Start()
    {
        playerMovement = topdMove.instance;
    }

    public void disableUIinteractions()
    {
        UiShown = true;
    }

    public void allowUIinteractions()
    {
        UiShown = false;
    }

    public void disableMovement()
    {
        playerMovement.freezeMovement();
    }
    public void allowMovement()
    {
        playerMovement.unfreezeMovement();
    }

}
