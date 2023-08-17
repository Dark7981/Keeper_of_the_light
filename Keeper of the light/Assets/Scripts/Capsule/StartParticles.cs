using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParticles : MonoBehaviour
{
    public ParticleSystem _particle;
   
    void Start()
    {
        _particle.Play();
        
    }

    
}
