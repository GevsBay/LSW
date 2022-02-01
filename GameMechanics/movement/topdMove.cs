using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topdMove : MonoBehaviour
{
    #region instance
    public static topdMove instance;

    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
        }
    }

    #endregion

    [SerializeField] Rigidbody2D rb;
    
    [SerializeField] float movementSpeed = 5f;
    bool canMove = true;

    Vector2 movement;
    bool facingRight = true;

    [Header("Animations")]
    public Animator animator;
    public string[] attackanimations;
    public float attackDelay = 3f;
    public float timer;

    void Update()
    {
        if (!canMove)
            return;

        if (timer > 0)
            timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer <= 0.1)
        {

            int random = Random.Range(0, attackanimations.Length);
            animator.CrossFadeInFixedTime(attackanimations[random], 0.1f);
            timer = attackDelay;
        }
       movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
       animator.SetFloat("x", Mathf.Abs(movement.x));
       animator.SetFloat("y", movement.y);
       animator.SetFloat("speed", movement.sqrMagnitude);
         
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);

        #region flipping Graphics

        if(movement.x > 0 && !facingRight)
        {
            flipGraphics();
        }
        if(movement.x < 0 && facingRight)
        {
            flipGraphics();
        }

        #endregion

    }

    void flipGraphics()
    {

        Vector3 currentScale = gameObject.transform.localScale;

        currentScale.x *= -1;

        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public void freezeMovement()
    {
        animator.SetFloat("speed", 0);
        timer = attackDelay / 2;

        canMove = false;
    }

    public void unfreezeMovement()
    {
        canMove = true;
    }

}