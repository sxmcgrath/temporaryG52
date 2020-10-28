
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

    [SerializeField] private GameObject menuDefaultCanvas;
    [SerializeField] private GameObject GeneralSettingsCanvas;
    [SerializeField] private GameObject menu_instruction;
    [SerializeField] private GameObject dialog_newStart;


    private void Update()
    {
        current_level = level_text.currentLevel;
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void MouseClick(string buttonType)
    {

        if (buttonType == "BACK")
        {
            GoBackToMainMenu();
        }


        else if (buttonType == "EXIT")
        {
            Application.Quit();
        }

        else if (buttonType == "SETTING")
        {
            menuDefaultCanvas.SetActive(false);
            GeneralSettingsCanvas.SetActive(true);
        }

        if (buttonType == "INSTRUCTION")
        {
            menuDefaultCanvas.SetActive(false);
            menu_instruction.SetActive(true);
        }


        if (buttonType == "START")
        {
            menuDefaultCanvas.SetActive(false);
            dialog_newStart.SetActive(true);
        }
    }
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
    }



    public void GoBackToMainMenu()
    {
        menuDefaultCanvas.SetActive(true);
        dialog_newStart.SetActive(false);
        GeneralSettingsCanvas.SetActive(false);
        menu_instruction.SetActive(false);
    }

}


