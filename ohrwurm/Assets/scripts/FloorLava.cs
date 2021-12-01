using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FloorLava : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        Debug.Log(other);

        if (other.gameObject.tag == "Player") 
        {
            other.gameObject.transform.parent.GetComponent<PlayerController>().InitDeath();
        }
    }

}
