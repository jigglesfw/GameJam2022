using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    private bool grounded = false;
    


    //Throw Stuff
    public float throwConstant = 1;
    private float throwForce;
    private float throwTimer = 0;
    public float throwTime;
    private bool holdingThrow = false;
    private int selectExplosive = 1;
    private List<GameObject> stickies = new List<GameObject>();

    //World 
    private Vector2 normalGrav;

    //Object Stuff
    private GameObject lastTouched;
    public GameObject Grenade;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGrav = Physics2D.gravity;
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        if (Input.GetKeyDown(KeyCode.Space))
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

        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 1 && number < 10)
            {
                selectExplosive = number-1;
            }
        }
    }

            private void Move()
    {
        H = Input.GetAxisRaw("Horizontal");
        V = Input.GetAxisRaw("Vertical");
        if (H != 0)
        {
            if (Mathf.Abs(rb.velocity.x) < hSpeed || Mathf.Sign(H) != Mathf.Sign(rb.velocity.x))
            {
                rb.velocity = new Vector2(H * hSpeed, rb.velocity.y);
            }
        }
        else
        {
            if(grounded == true) { rb.velocity = new Vector2(rb.velocity.x * 0.95f, rb.velocity.y); }
        }
        if (V < 0) { Physics2D.gravity = normalGrav * 1.5f; }
        else { Physics2D.gravity = normalGrav; }

        if (rb.velocity.y > vSpeed) { rb.velocity = new Vector2(rb.velocity.x , rb.velocity.y - 0.5f); }

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
            grounded = true;
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground") { lastTouched = null; }
        grounded = false;
        canJump = false;
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
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(selectExplosive == 1 && stickies.Count >= 3) { Destroy(stickies[0].gameObject);  stickies.RemoveAt(0); }
            var newGrenade = Instantiate(Grenade, transform.position, Quaternion.identity);
            var grenadeScript = newGrenade.GetComponent<Projectile>();
            grenadeScript.type = (Projectile.ProjectileType)selectExplosive;
            Vector3 difference = new Vector2(mousePos.x - newGrenade.transform.position.x, mousePos.y - newGrenade.transform.position.y);

            newGrenade.transform.position = newGrenade.transform.position + difference.normalized;
            newGrenade.GetComponent<Rigidbody2D>().velocity = difference.normalized * throwConstant* throwForce;
            Physics2D.IgnoreCollision(newGrenade.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            if(grenadeScript.type == Projectile.ProjectileType.Sticky) { stickies.Add(newGrenade.gameObject); }
            holdingThrow = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Explosion")
        {
            rb.AddForce((new Vector2(transform.position.x - collision.transform.position.x , transform.position.y - collision.transform.position.y).normalized + new Vector2(0, 0.1f)) * collision.transform.GetComponent<Explosion>().explosiveForce);
        }
    }
}
