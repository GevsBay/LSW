using UnityEditor;

#if (UNITY_EDITOR)
public class fixdatabaseId : ScriptableWizard
{
    //Makes so we can ignore assigning id when adding element to database

    public itemdatabase database;

    [MenuItem("Editor/ReAssignId")]


    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Fix Id", typeof(fixdatabaseId), "Apply"); 
    }

    void OnWizardCreate()
    { 
        for (int i = 0; i < database.itemsDatabase.Length; i++)
        {
            database.itemsDatabase[i].itemid = i + 1;
        }

    }
}
#endif