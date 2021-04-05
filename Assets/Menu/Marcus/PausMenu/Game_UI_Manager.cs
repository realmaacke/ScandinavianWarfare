using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;
    
                        //Marcus
public class Game_UI_Manager : MonoBehaviour
{   //Variables
    [Header("Ingame Variables")]
    public GameObject PausMenu;
    public GameObject GameUI;
    public GameObject MovementSystem;
    public static bool GameIsPaused = false;

    [Header("Menu Variables")]
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public void BackToMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MovementSystem.GetComponent<MovementController>().enabled = true;
        PausMenu.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MovementSystem.GetComponent<MovementController>().enabled = false;
        PausMenu.SetActive(true);
        GameUI.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MovementSystem.GetComponent<MovementController>().enabled = true;
        PausMenu.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Settings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void back()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
