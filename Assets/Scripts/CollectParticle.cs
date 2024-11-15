using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectParticle : MonoBehaviour
{

    public ParticleSystem part;

    void OnParticleCollision(GameObject other) {
        if(other.tag == "Player")
           Debug.Log(other.tag);
    }
    
}
