using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    //Player Config
    public float hSpeed;
    public float vSpeed;
    public float jumpForce;
    private float runMultiplier;
    private float H;
    private float V;
    private Rigidbody2D rb;
    public static Vector2 playerPos;
    public static Vector2 playerVel;
    private bool canJump;
    private bool hasJumped;

    //Object Stuff
    private GameObject lastTouched;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move()
    {
        H = Input.GetAxisRaw("Horizontal");
        V = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(H * hSpeed, rb.velocity.y);

    }

    private void Jump()
    {
        if(canJump == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" )
        {
            lastTouched = null;
            canJump = true;
        }
        if (collision.transform.tag == "Wall")
        {
 
            if(collision.gameObject != lastTouched)
            {
                lastTouched = collision.transform.gameObject;
                canJump = true;
            }
        }
    }
}
