using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;

    public void LoseLife()
    { 
        //Decrease the value of livesRemaining
        livesRemaining--;

        //Hide  every lost heart image 
        if (livesRemaining>=0)
            lives[livesRemaining].enabled = false;

        //If lives remaing equals to 0 
        if (livesRemaining == 0)
        {
            //Call defeat
            Defeat();
        }
    }

    void Defeat()
    {
        SceneManager.LoadScene(4);
    }
}