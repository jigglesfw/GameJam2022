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

    //Throw Stuff
    public float throwConstant = 1;
    private float throwForce;
    private float throwTimer = 0;
    public float throwTime;
    private bool holdingThrow = false;

    //Object Stuff
    private GameObject lastTouched;
    public GameObject Grenade;

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
        if (Input.GetMouseButtonDown(0))
        {
            throwTimer = 0;
            holdingThrow = true;
        }
        if (holdingThrow == true)
        {
            ThrowProjectile();
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
    private void ThrowProjectile()
    {
        if (throwTimer <= throwTime)
        {
            throwTimer += Time.deltaTime;
            throwForce = throwTimer / throwTime;
            //Debug.Log(throwForce);
        }
        if(Input.GetMouseButtonUp(0))
        {
            var newGrenade = Instantiate(Grenade, transform.position, Quaternion.identity);
            Debug.Log(throwForce);
            newGrenade.GetComponent<Rigidbody2D>().velocity = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - newGrenade.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - newGrenade.transform.position.y).normalized * throwConstant* throwForce;
            Physics2D.IgnoreCollision(newGrenade.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            holdingThrow = false;
        }

    }
}
