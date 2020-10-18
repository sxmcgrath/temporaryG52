using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPaused : MonoBehaviour
{

    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject player;




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            print(889);
            // if (isGamePaused)
            // {
            //     player.GetComponent<FirstPersonCam>().gamePlaying();
            //     pauseMenu.SetActive(false);
            //     isGamePaused = false;
            // }
            // else
            // {
            gamePause();
            // }
        }
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
