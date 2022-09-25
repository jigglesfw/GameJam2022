using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintExplosion : MonoBehaviour
{
    public ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if(particles.gravityModifier > 0.1f) { particles.gravityModifier -= Time.deltaTime * 7; }
=======
        if(particles.gravityModifier > 0.1f) { particles.gravityModifier -= Time.deltaTime * 5; }
>>>>>>> joshua
        
    }

    
}
