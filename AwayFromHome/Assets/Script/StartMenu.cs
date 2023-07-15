using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{ 
    void Start()
    {
        //Lock cursor
        Cursor.lockState = CursorLockMode.None;
        //Make it invisible
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        //Will take us to the next scene
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        //Will close the app
        Application.Quit();
    }
}
