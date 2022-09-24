using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float timer = 0.1f;
    public float explosiveForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(timer > 0) { timer -= Time.deltaTime; }  
         else { Destroy(gameObject); }
    }
}
