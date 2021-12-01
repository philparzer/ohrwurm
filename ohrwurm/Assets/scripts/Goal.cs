using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    public GameMeta gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //log on collision when box collider on gameobject is triggered
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   
            gameManager.Win();
            // //reload current scene
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
