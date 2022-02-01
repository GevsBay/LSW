using UnityEngine.UI; 
using UnityEngine;

public class saleItem : MonoBehaviour
{
    public Image icon;
    public new Text name;
    public Text pricetext;
    public int price; 
    public int id;
    public void highlight() 
    {
        shopManager.instance.highlightPurchase(this);
    }
}
