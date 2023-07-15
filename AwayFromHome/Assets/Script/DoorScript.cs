using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    void victory ()
    {
        SceneManager.LoadScene(5);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //When door will collide with player, show the victory Ui
        if (collision.tag == "Player")
        {
            victory();
        }
    }
}
