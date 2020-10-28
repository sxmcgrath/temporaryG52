
/**
	The idea and skeleton of the codes are adapted from the Unity Asset Stor [Full Main Menu System]:
    The link can be found here: https://assetstore.unity.com/packages/tools/gui/full-menu-system-free-158919
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// namespace SpeedTutorMainMenuSystem

public class MenuController : MonoBehaviour
{


    private static int current_level = 1;


    [Header("Default Menu Values")]


    [Header("Levels To Load")]

    private int menuNumber;

    [Header("Main Menu Components")]
    [SerializeField] private GameObject menuDefaultCanvas;
    [SerializeField] private GameObject GeneralSettingsCanvas;
    [SerializeField] private GameObject menu_instruction;
    [Space(10)]
    [Header("Menu Popout Dialogs")]

    [SerializeField] private GameObject dialog_newStart;
    // #endregion

    // // #region Slider Linking
    // [Header("Menu Sliders")]
    // public float controlSenFloat = 2f;
    // [Space(10)]

    // [SerializeField] private Text volumeText;
    // [SerializeField] private Slider volumeSlider;
    // [Space(10)]
    // [SerializeField] private Toggle invertYToggle;
    private void Start()
    {
        menuNumber = 1;
    }

    private void Update()
    {

        current_level = level_text.currentLevel;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuNumber == 2 || menuNumber == 7 || menuNumber == 8)
            {
                GoBackToMainMenu();
                ClickSound();
            }

            else if (menuNumber == 3 || menuNumber == 4 || menuNumber == 5)
            {
                ClickSound();
            }

        }
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    // #region Menu Mouse Clicks
    public void MouseClick(string buttonType)
    {

        if (buttonType == "BACK")
        {
            // GeneralSettingsCanvas.SetActive(false);
            print(4);
            GoBackToMainMenu();
            menuNumber = 4;
        }


        if (buttonType == "EXIT")
        {
            print("exit the game");
            Application.Quit();
        }

        if (buttonType == "Options")
        {
            print(2);
            menuDefaultCanvas.SetActive(false);
            GeneralSettingsCanvas.SetActive(true);
            menuNumber = 2;
        }

        if (buttonType == "INSTRUCTION")
        {
            menuDefaultCanvas.SetActive(false);
            menu_instruction.SetActive(true);
            menuNumber = 2;
        }


        if (buttonType == "START")
        {
            print(7);
            menuDefaultCanvas.SetActive(false);
            dialog_newStart.SetActive(true);
            menuNumber = 7;
        }
    }
    // #endregion

    public void VolumeSlider(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ClickNewGameDialog(string ButtonType)
    {
        if (ButtonType == "Yes")
        {
            SceneManager.LoadScene(current_level);
        }

        if (ButtonType == "No")
        {
            GoBackToMainMenu();
        }
    }



    public void GoBackToMainMenu()
    {
        print(998);
        menuDefaultCanvas.SetActive(true);
        dialog_newStart.SetActive(false);
        GeneralSettingsCanvas.SetActive(false);
        menu_instruction.SetActive(false);
        menuNumber = 1;
    }

}


