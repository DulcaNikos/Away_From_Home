using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    bool isGamePaused = false;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;

    PlayerMovement playerMovement;

    EnemyAI[] enemies;


    // Start is called before the first frame update
    void Start()
    {
        //Find and declare players and enemy scripts
        playerMovement = FindObjectOfType<PlayerMovement>();
        enemies = FindObjectsOfType<EnemyAI>();

        if (pauseMenu) pauseMenu.SetActive(false);
        if (optionsMenu) optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If we press escape button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //And the game is paused
            if (isGamePaused)
            {
                //Resume the game
                ResumeGame();
            }
            else
            {
                //Else, pause the game
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        //We set the pause menu true
        if (pauseMenu) pauseMenu.SetActive(true);
        //Handle the players pause 
        if (playerMovement) playerMovement.HandlePause();
        //Handle enemies pause
        foreach (EnemyAI enemy in enemies) enemy.HandlePause();
        //Make the bool isgamepaused true
        isGamePaused = true;
        //Freeze time
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        //Now make it false again
        if (pauseMenu) pauseMenu.SetActive(false);
        //Handle players pause
        if (playerMovement) playerMovement.HandlePause();
        //Handle enemies pause
        foreach (EnemyAI enemy in enemies) enemy.HandlePause();
        //Set options menu false
        if (optionsMenu) optionsMenu.SetActive(false);
        //Make the bool isgamepaused again false
        isGamePaused = false;
        //Unfreeze time
        Time.timeScale = 1f;

        EventSystem.current.SetSelectedGameObject(null);
    }

    void Restart()
    {
        //Restart the Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Options()
    {
        //Set options menu true
        if (optionsMenu) optionsMenu.SetActive(!optionsMenu.activeSelf);

        EventSystem.current.SetSelectedGameObject(null);
    }

    void Quit()
    {
        SceneManager.LoadScene(2);
    }
}
