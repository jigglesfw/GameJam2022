using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rb;
    public GameObject explosion;
    public float explosionTime = 3;
    private float explosionTimer;
    public enum ProjectileType
    {
        Grenade,
        Rocket,
        Idk
    }

    public ProjectileType type;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        explosionTimer = explosionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (explosionTimer >= 0)
        {
            explosionTimer -= Time.deltaTime;
        }
        else
        {
            var newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
    }
}
