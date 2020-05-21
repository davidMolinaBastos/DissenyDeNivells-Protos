using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [Header("Stats")]
    public float movementSpeed = 10f;
    public float jumpForce = 20f;
    [Range(0, 1)] public float airControl = 0.9f;
    public float groundDetectRadious = 0.5f;

    [Header("KeyCodes")]
    public KeyCode jumpKeyCode = KeyCode.Space;

    bool onGround = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        Camera.main.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, -10);
        if (onGround && Input.GetKeyDown(jumpKeyCode))
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        onGround = Physics2D.OverlapCircle(transform.position, groundDetectRadious);
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * movementSpeed, 0), ForceMode2D.Force);
    }
}
