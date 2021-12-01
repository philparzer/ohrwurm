using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //TODO: subscribe to game start event and start disable enable timer
    }

    // Update is called once per frame
    void Update()
    {
        //disable enable timer
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent.GetComponent<PlayerController>().InitDeath();
        }

    }

    

}
