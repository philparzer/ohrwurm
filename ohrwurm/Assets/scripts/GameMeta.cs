using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMeta : MonoBehaviour
{



    public Cinemachine.CinemachineVirtualCamera endCam;
    
    public Canvas gameOverUI;
    public Canvas wonUI;
    public Canvas pauseUI;
    public Canvas gameUI;
    public Canvas placementUI;
    public float time;

    public bool gameRunning = true;


    void Start(){

        gameOverUI.enabled = false;
        wonUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GameOver()
    {
        gameRunning = false;
        gameOverUI.enabled = true;
        endCam.Priority = 12;

    }

    public void Win()
    {
        gameRunning = false;
        wonUI.enabled = true;
        endCam.Priority = 12;
    }

    public void BackToMenu(){
        SceneManager.LoadScene(0);
    }

}
