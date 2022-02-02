using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class treasure : interactable
{
    public int itemRequired;
    public spawnItem[] possibleItems;
    public Sprite openSprite;
    public SpriteRenderer icon;
    inventory inv;

    public override void initialize()
    {
        icon = GetComponent<SpriteRenderer>();
        inv = inventory.instance;
    }

    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetMouseButtonDown(0))
                interact();
        }
    }

    public override void interact()
    {
        if (interactionComplete)
            return;
          
        if(!inv.currentInventory.Any(x => x.itemid == itemRequired))
            return;

        base.interact(); 

        for (int i = 0; i < possibleItems.Length; i++)
        {
            pickupobj obj = Instantiate(possibleItems[i].prefab, transform.position, Quaternion.identity).GetComponent<pickupobj>();
            obj.itemid = possibleItems[i].itemId;
        }
        inv.removeItem(itemRequired);
        icon.sprite = openSprite;
    }
}
