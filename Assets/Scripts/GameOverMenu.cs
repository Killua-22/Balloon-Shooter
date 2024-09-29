using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RetryGame()
    {
        // Reload the game scene (replace "SampleScene" with your actual scene name)
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu");
    }
}
