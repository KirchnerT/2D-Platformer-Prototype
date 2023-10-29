using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;

    private float inputHorizontal;
    private float inputVertical;

    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private LayerMask whatIsLadder;
    public float distance = 0.5f;
    private bool isClimbing;

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float jumpSpeed = 7f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputHorizontal * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && (IsGrounded() || isClimbing))
        {
            isClimbing = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        if (hitInfo.collider != null)
        {
            float direction = Input.GetAxisRaw("Vertical");
            if (direction != 0)
            {
                isClimbing = true;
            }
        } else
        {
            isClimbing = false;
        }

        if (isClimbing == true)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * moveSpeed);
            rb.gravityScale = 0;
        } else
        {
            rb.gravityScale = 1.5f;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .2f, groundLayers) && rb.velocity.y == 0;
    }
}
