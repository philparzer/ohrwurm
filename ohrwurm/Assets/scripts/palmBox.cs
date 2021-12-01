using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class palmBox : MonoBehaviour
{

    public Transform palm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = palm.position;
        transform.rotation = palm.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent.GetComponent<PlayerController>().InitDeath();
        }
    }

}
