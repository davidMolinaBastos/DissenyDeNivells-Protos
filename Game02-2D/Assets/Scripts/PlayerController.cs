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
    public float attackTime = 1f;
    public float maxLife = 100f;

    [Header("KeyCodes")]
    public KeyCode jumpKeyCode = KeyCode.Space;
    public KeyCode attackKeyCode = KeyCode.Mouse0;

    public BoxCollider2D sword;

    float HP = 100f;
    float attackCT = 0;
    bool onGround = false;
    bool inmunity = false;
    private void Start()
    {
        print(jumpForce);
        jumpForce = 10;
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (attackCT > 0)
            attackCT += Time.deltaTime;
        else if (sword.enabled)
            sword.enabled = false;

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        if (Input.GetKeyDown(attackKeyCode) && attackCT > 0)
        {
            sword.enabled = true;
            attackCT = attackTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollisionValid(collision))
            onGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
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
        }
        if (rb.velocity.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if(rb.velocity.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;
    }

    public void Damage(float damage)
    {
        if (inmunity)
            return;
        HP -= damage;
        if (HP >= 0)
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver();
    }
    public float GetHP(){return HP;}
    public void InmunityToggle(){ inmunity = !inmunity;}
    public bool GetInmunity() { return inmunity; }
}
