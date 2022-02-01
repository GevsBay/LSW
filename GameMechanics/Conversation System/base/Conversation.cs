using System.Collections;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct dialogue
{
    public bool lastLine;
    public bool actionLine;
     
    public enum speaker { player, npc};
    public speaker orator;
     
    [TextArea]
    public string speechContent; 
}

[CreateAssetMenu(fileName = "New", menuName = "Conversations")]
public class Conversation : ScriptableObject
{
    public string shortDescription;

    public dialogue[] dialogues;
    public int currentindex = 0;
    public enum action { buy, sell };
    public action _action;
    public bool Action = false; 
}
