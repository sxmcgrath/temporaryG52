using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpeedTutorMainMenuSystem
{
    public class MenuController : MonoBehaviour
    {

        // 


        private static int current_level = 1;







        // 
        // #region Default Values
        [Header("Default Menu Values")]


        [Header("Levels To Load")]

        private int menuNumber;
        // #endregion

        // #region Menu Dialogs
        [Header("Main Menu Components")]
        [SerializeField] private GameObject menuDefaultCanvas;
        [SerializeField] private GameObject GeneralSettingsCanvas;
        [SerializeField] private GameObject soundMenu;
        [SerializeField] private GameObject gameplayMenu;
        [SerializeField] private GameObject confirmationMenu;
        [Space(10)]
        [Header("Menu Popout Dialogs")]

        [SerializeField] private GameObject dialog_newStart;
        // #endregion

        // #region Slider Linking
        [Header("Menu Sliders")]
        public float controlSenFloat = 2f;
        [Space(10)]

        [SerializeField] private Text volumeText;
        [SerializeField] private Slider volumeSlider;
        [Space(10)]
        [SerializeField] private Toggle invertYToggle;
        // #endregion

        // #region Initialisation - Button Selection & Menu Order
        private void Start()
        {
            menuNumber = 1;
        }
        // #endregion

        //MAIN SECTION
        public IEnumerator ConfirmationBox()
        {
            confirmationMenu.SetActive(true);
            yield return new WaitForSeconds(2);
            confirmationMenu.SetActive(false);
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
                    GoBackToOptionsMenu();
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
                // soundMenu.SetActive(true);
                print(4);
                GoBackToMainMenu();
                menuNumber = 4;
            }


            if (buttonType == "Exit")
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
            volumeText.text = volume.ToString("0.0");
        }

        public void VolumeApply()
        {
            PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
            Debug.Log(PlayerPrefs.GetFloat("masterVolume"));
            StartCoroutine(ConfirmationBox());
        }



        // #region Dialog Options - This is where we load what has been saved in player prefs!
        public void ClickNewGameDialog(string ButtonType)
        {
            if (ButtonType == "Yes")
            {
                // SceneManager.LoadScene(_newGameButtonLevel);
                SceneManager.LoadScene(current_level);
            }

            if (ButtonType == "No")
            {
                GoBackToMainMenu();
            }
        }

        // #region Back to Menus
        public void GoBackToOptionsMenu()
        {
            GeneralSettingsCanvas.SetActive(true);
            soundMenu.SetActive(false);
            gameplayMenu.SetActive(false);
            VolumeApply();

            menuNumber = 2;
        }

        public void GoBackToMainMenu()
        {
            print(998);
            menuDefaultCanvas.SetActive(true);
            dialog_newStart.SetActive(false);
            GeneralSettingsCanvas.SetActive(false);
            soundMenu.SetActive(false);
            gameplayMenu.SetActive(false);
            menuNumber = 1;
        }

    }

}
