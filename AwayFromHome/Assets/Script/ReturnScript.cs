using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScript : MonoBehaviour
{
    void Start()
    {
        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        //Make it invisible
        Cursor.visible = false;
    }

    void Update()
    {
        //Will take us to the previous scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }
    }
}