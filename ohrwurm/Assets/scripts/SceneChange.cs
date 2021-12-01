using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  
public class SceneChange: MonoBehaviour {  
    public void StartGame() {  
        Debug.Log("Start Game");
        SceneManager.LoadScene(1);  
    }

    public void QuitGame() {
        Application.Quit();
    }
    public void OpenExtLink() {
        Application.OpenURL("https://itch.io/jam/game-off-2021");
    }

}  