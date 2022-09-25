using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rb;
    public GameObject explosion;
    private float explosionTime;
    private float explosionTimer;
    private float explosionRadius;
    private float explosionForce;
    public List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer spR;
    public enum ProjectileType
    {
        Grenade,
        Sticky,
        Rocket
    }

    public ProjectileType type;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spR = GetComponent<SpriteRenderer>();

        switch(type)
        {
            case ProjectileType.Grenade:
                explosionTime = 2;
                explosionRadius = 8;
                explosionForce = 1500;
                spR.sprite = sprites[0];
                if (transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x) { rb.AddTorque(10); }
                else { rb.AddTorque(-10); }
                
                break;

            case ProjectileType.Sticky:
                explosionTime = Mathf.Infinity;
                explosionRadius = 4;
                explosionForce = 500;
                spR.sprite = sprites[1];
                break;

            case ProjectileType.Rocket:
                spR.sprite = sprites[2];
                break;
        }
        explosionTimer = explosionTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (explosionTimer >= 0)
        {
            if(Input.GetMouseButton(1) && type == ProjectileType.Sticky) { Explode(); }
            explosionTimer -= Time.deltaTime;
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        newExplosion.GetComponent<Explosion>().explosiveForce = explosionForce;
        newExplosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, 0);
        Destroy(transform.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Projectile")
        {
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), collision.transform.GetComponent<Collider2D>());
        }
        else
        {
            if (type == ProjectileType.Sticky)
            {
                transform.parent = collision.transform;
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
    }
}
