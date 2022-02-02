using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupobj : MonoBehaviour
{
    public int itemid; 
    private float spawndelay = 0.6f;
     
    inventory inv;

    public void Start()
    { 
        inv = inventory.instance;
    }

    private void Update()
    {
        if (spawndelay > 0)
            spawndelay -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (spawndelay > 0)
            return;
        if (collision.gameObject.CompareTag("Player"))
        {
            inv.addItem(itemid);
            Destroy(gameObject);
        }
    }
}
