using UnityEngine;
using UnityEngine.SceneManager;

public class mainMenu : MonoBehaviour
{


    void startPlaying()
    {
        SceneManager.LoadScene(1);
    }

    void exitGame()
    {
        Application.Quit();
    }


}