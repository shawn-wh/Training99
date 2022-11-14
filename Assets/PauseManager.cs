using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool isGamePaused = false; 

    [SerializeField] GameObject PauseMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame(); 
            }
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;    // Paused 
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;    // Resume 
        isGamePaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;    // Resume 
        isGamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");

        // Whenever load a new scence, need to change timeScale to normal
        Time.timeScale = 1f;
    }
}
