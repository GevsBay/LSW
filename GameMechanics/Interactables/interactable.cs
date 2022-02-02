using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class interactable : MonoBehaviour
{
    CircleCollider2D col;
    public float radius = 0.7f;
    public bool canInteract = false;
    public bool interactionComplete = false;

    itemdatabase database;


    private void Start()
    {
        database = itemdatabase.instance;

        col = GetComponent<CircleCollider2D>();
        col.radius = radius;
        col.isTrigger = true;
        col.offset = Vector2.zero;
        initialize();
    }

    public virtual void initialize()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    } 

    public virtual void interact() 
    {
        if (!interactionComplete)
            interactionComplete = true;

    }
}
