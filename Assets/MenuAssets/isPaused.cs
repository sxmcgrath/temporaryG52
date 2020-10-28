using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class isPaused : MonoBehaviour
{

    public static bool isGamePaused = false;
    public static bool isInstruction = false;
    public GameObject pauseMenu;
    public GameObject player;
    [SerializeField] private GameObject menu_instruction;


    public void MouseClick(string buttonType)
    {
        if (buttonType == "RESUME")
        {
            gameResume();
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
            SceneManager.LoadScene(0);
        }

        else if (buttonType == "INSTRUCTION")
        {

            if (isInstruction)
            {
                menu_instruction.SetActive(false);
                isInstruction = false;
            }
            else
            {
                menu_instruction.SetActive(true);
                isInstruction = true;
            }
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
