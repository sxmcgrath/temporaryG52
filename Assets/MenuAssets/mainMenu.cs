using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{


    public void startPlaying()
    {
        SceneManager.LoadScene(1);
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void exitGame()
    {
        Application.Quit();
    }


}