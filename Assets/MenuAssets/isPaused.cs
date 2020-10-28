using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class isPaused : MonoBehaviour
{

    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject player;


    public void MouseClick(string buttonType)
    {
        if (buttonType == "RESUME")
        {
            player.GetComponent<FirstPersonCam>().gameResuming();
            pauseMenu.SetActive(false);
            isGamePaused = false;
        }
        else if (buttonType == "RESTART")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameResume();
        }
        else if (buttonType == "WIN")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (buttonType == "QUIT")
        {
            print(0);
            // isGamePaused = false;
            SceneManager.LoadScene(0);
        }
    }


    void Start()
    {
        gameResume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            if (isGamePaused)
            {
                gameResume();
            }
            else
            {
                gamePause();
            }
        }
    }


    void gameResume()
    {
        player.GetComponent<FirstPersonCam>().gameResuming();
        pauseMenu.SetActive(false);
        isGamePaused = false;
    }


    void gamePause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<FirstPersonCam>().gamePausing();
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }
}
