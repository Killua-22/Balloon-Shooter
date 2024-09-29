using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RetryGame()
    {
        
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMainMenu()
    {
        
        SceneManager.LoadScene("Main Menu");
    }
}
