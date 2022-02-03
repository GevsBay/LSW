using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MessageManager : MonoBehaviour
{
    #region instance
    public static MessageManager instance;

    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
    }

    #endregion

    #region UI
    public CanvasGroup canvas;
    public Text message;

    bool poping;
    public float duration;
    float timer;
    public Animator anim;
    #endregion

    public string error_roadbuild;
     

    public void popout(string text)
    {
        poping = true;
        anim.CrossFadeInFixedTime("pop", 0.01f);
        StopAllCoroutines();
        StartCoroutine(scrollText(text));
    }

    IEnumerator scrollText(string text) {
        message.text = ""; 
        yield return new WaitForSecondsRealtime(0.5f); 
        foreach (char c in text)
        {
            message.text += c;

            yield return new WaitForSecondsRealtime(0.02f);
        }
        StartCoroutine(closeMessage()); 

    }

    IEnumerator closeMessage() 
    {
        yield return new WaitForSecondsRealtime(3f);
        message.text = "";
        anim.CrossFadeInFixedTime("popout", 0.01f);
    }
}
