using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody2D rb;
    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case ProjectileType.Grenade:
                break;
            case ProjectileType.Rocket:
                break;
            case ProjectileType.Idk:
                break;

        }
    }





}
