using UnityEditor;

#if (UNITY_EDITOR)
public class createsubarrays : ScriptableWizard
{
    //used for creating reference array by useful item type not to go through all the database trying to find the id of the item and not 
    //risking performance issue of having different arrays in database for scripts
    
    public itemdatabase database;

    [MenuItem("Editor/Create Sub arrays")]
     
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Item Arrays By Type", typeof(createsubarrays), "Apply");
    }

    void OnWizardCreate()
    {
        database.questItems.Clear();
        database.clothing.Clear();
        database.weapons.Clear();


        for (int i = 0; i < database.itemsDatabase.Length; i++)
        {
            switch (database.itemsDatabase[i].itemType)
            {
                case item.type.questitem:
                    database.questItems.Add(database.itemsDatabase[i]);
                    break;
                case item.type.weapon:
                    database.weapons.Add(database.itemsDatabase[i]);
                    break;  
                default:
                    database.clothing.Add(database.itemsDatabase[i]); 
                    break;
            }
        }

    }
}
#endif
