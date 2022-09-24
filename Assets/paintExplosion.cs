using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintExplosion : MonoBehaviour
{
    public ParticleSystem particles;
    public float timeTillFreeze;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = timeTillFreeze;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0) { timer -= Time.deltaTime; }
        else { particles.Pause(); }
        
    }

    
}
