using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float timer = 0.1f;
    public float explosiveForce;
    public GameObject splatter;
    private Camera mCam;
    private bool stopIt = false;
    private SpriteRenderer mRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
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
            if (stopIt == false)
            {
                var particler = Instantiate(splatter, transform.position, Quaternion.identity);
                if (explosiveForce >= 1500) { particler.GetComponent<ParticleSystem>().Emit(400); }
                else if (explosiveForce > 500) { particler.GetComponent<ParticleSystem>().Emit(10); }
                else { particler.GetComponent<ParticleSystem>().Emit(30); }
                GetComponent<Collider2D>().enabled = false;
                stopIt = true;
            }
            if (GetComponent<AudioSource>().isPlaying == false && GetComponent<SpriteRenderer>().color.a <= 0) { Destroy(gameObject); } }
        mRenderer.color = new Color(mRenderer.color.r, mRenderer.color.g, mRenderer.color.b, mRenderer.color.a - Time.deltaTime * 2);


    }
}
