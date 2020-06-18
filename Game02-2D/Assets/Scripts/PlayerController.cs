using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Stats")]
    public float movementSpeed = 10f;
    public float maxSpeed = 10f;
    public float jumpForce = 20f;
    [Range(0, 1)] public float airControl = 0.9f;
    //public float groundDetectRadious = 0.5f;

    [Header("KeyCodes")]
    public KeyCode jumpKeyCode = KeyCode.Space;

    /*
    [Header("References")]
    public LayerMask player;
    public LayerMask ground;*/
    bool onGround = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        /*
        if (onGround && Input.GetKeyDown(jumpKeyCode))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            onGround = false;
        }

        float moveVelocity = rb.velocity.x;
        if (Input.GetAxis("Horizontal") < 0)
            moveVelocity = -movementSpeed;
        else if (Input.GetAxis("Horizontal") > 0)
            moveVelocity = movementSpeed;
        else
            moveVelocity = 0;
        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, groundDetectRadious, ground.value);
        for (int i = 0; i < col.Length; i++)
            if (col[i])
                onGround = true;
        print(onGround);*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollisionValid(collision))
            onGround = true;
        print(onGround);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        print(onGround);
    }
    private bool CollisionValid(Collision2D col)
    {
        bool val = false;
        foreach (ContactPoint2D c in col.contacts)
        {
            Vector2 col_dir_vector = c.point - rb.position;
            if (col_dir_vector.y < 0)
                val = true;
        }
        return val;
    }
    private void FixedUpdate()
    {
        float x_movement = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude < maxSpeed)
        {
            Vector2 movement = new Vector2(x_movement, 0);
            if(!onGround)
                rb.AddForce(movementSpeed * airControl * movement);
            else
                rb.AddForce(movementSpeed * movement);
        }
        if (x_movement == 0)
            rb.velocity -= new Vector2(rb.velocity.x * 0.5f,0);
        if (onGround && Input.GetKeyDown(jumpKeyCode))
        {
            rb.velocity += Vector2.up * jumpForce;
            float x = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
            float y = Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed);
            rb.velocity = (new Vector2(x,y));
            //rb.AddForce(Vector2.up * jumpForce);
        }
        if (rb.velocity.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if(rb.velocity.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;
    }
}
