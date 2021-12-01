using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class ParticleResponse : MonoBehaviour
{
    public ParticleSystem freezeSpray;
    private float collisionTimeout = 5f;
    private bool collidedFreeze = false;


    void Update() 
    {   
        if (collidedFreeze)
        {
            collisionTimeout -= Time.deltaTime;

            if (collisionTimeout <= 0)
            {
                collidedFreeze = false;
                collisionTimeout = 5f;
            }
        }

    }

    private void OnParticleCollision(GameObject other) {


        if (other.GetComponent<ParticleSystem>() == freezeSpray)
        {
            if (!collidedFreeze)
            {
                transform.parent.GetComponent<PlayerController>().frozenTime += 3f;
                collidedFreeze = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Palm")
        {
            Debug.Log("hit");
        }
    }
}
