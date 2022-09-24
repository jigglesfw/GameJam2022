using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
