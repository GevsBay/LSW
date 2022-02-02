using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New", menuName = "Quest")]
public class quest : ScriptableObject
{
    public bool questCompleted = false;
    public string questDescription;

    public int[] questStartObjects; 

    public int[] rewardObjects;
    public int[] lostObjects;

    public int moneyRewarded;
    public int MoneyDeducted;


    public string defaultInProgressLine = "Still Waiting Mate!";
    public string SuccessLine = "Great Job Mate!";

    public List<Conversation> relatedconversations;
}
