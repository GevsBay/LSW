using System.Collections;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct dialogue
{
    public bool lastLine;
     
    public enum speaker { player, npc};
    public speaker orator;
     
    [TextArea]
    public string speechContent; 
}

[CreateAssetMenu(fileName = "New", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public string shortDescription;

    public dialogue[] dialogues;
    public int currentindex = 0;
    public Conversation[] toUnlock;
    public bool finished = false;
    public bool repeater = false;
    public bool unlocked = false;

    public enum action { buy, sell, quest, questEnd };
    public action _action;
    public bool Action = false;
    public quest quest;
}
