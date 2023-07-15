using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikesScript : MonoBehaviour
{

    void Defeat()
    {
        SceneManager.LoadScene(4);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //When spikes will collide with player, show the Deafeat Ui
        if (collision.tag == "Player")
        {
            Defeat();
        }
    }
}
