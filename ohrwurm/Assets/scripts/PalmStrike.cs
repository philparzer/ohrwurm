using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PalmStrike : MonoBehaviour
{   
    public GameMeta gameManager;
    public GameObject palmPos;
    public bool lerping = false;
    public float handTimeout = 10f;
    public int chance = 99;

    public Vector3 lastPlayerPos;

    // Update is called once per frame

    void Awake() 
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if (!gameManager.gameRunning){return;}

        if (!lerping)
        {
            handTimeout -= Time.deltaTime;
            

            if (handTimeout <= 0)
            {
                DecideIfStrike();
                handTimeout = 10f;
            }
        }
        
        else 
        {
            
            StartCoroutine(WaitForStrike());
        }   
    }

    IEnumerator WaitForStrike()
    {
        
        yield return new WaitForSeconds(2);
        transform.position = Vector3.Lerp(transform.position, lastPlayerPos, Time.deltaTime * 10);
        StartCoroutine(WaitAfterStrike());
    }

    IEnumerator WaitAfterStrike()
    {
        yield return new WaitForSeconds(2);
        transform.position = Vector3.Lerp(transform.position, palmPos.transform.position, Time.deltaTime * 10);
        StartCoroutine(WaitForRenderDisable());
    }

    IEnumerator WaitForRenderDisable()
    {
        yield return new WaitForSeconds(.05f);
        GetComponent<MeshRenderer>().enabled = false;
        lerping = false;
    }

    void DecideIfStrike()
    {
        int random = Random.Range(0, 100);
        if (random <= chance)
        {
            GetComponent<MeshRenderer>().enabled = true;
            Strike();
        }

        else 
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

    }

    void Strike()
    {
        //move slowly to parent transform position
        lerping = true;
        lastPlayerPos = transform.parent.position;

    }


}