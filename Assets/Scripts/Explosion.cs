using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float timer = 0.1f;
    public float explosiveForce;
    public GameObject splatter;
    private Camera mCam;
    // Start is called before the first frame update
    void Start()
    {
        mCam = Camera.main;
        mCam.GetComponent<ShakeBehavior>().shakeTimer += 0.2f;
        if (explosiveForce > 500) { mCam.GetComponent<ShakeBehavior>().shakeAmount += 0.3f; }
        else { mCam.GetComponent<ShakeBehavior>().shakeAmount += 0.1f; }
        
    }

    // Update is called once per frame
    void Update()
    {
         if(timer > 0) { timer -= Time.deltaTime; }  
         else {
            var particler =  Instantiate(splatter, transform.position, Quaternion.identity);
            if(explosiveForce > 500) { particler.GetComponent<ParticleSystem>().Emit(300); }
            else { particler.GetComponent<ParticleSystem>().Emit(20); }
            
            
            Destroy(gameObject); }
    }
}
