using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f; // Timer starting value in seconds
    public TextMeshProUGUI timerText;  // UI Text element to display the time (using TextMeshPro if needed)

    private bool timerIsRunning = true;

    private void Update()
    {
        // Check if the timer is still running
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Reduce timeRemaining by deltaTime each frame
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(timeRemaining);
            }
            else
            {
                // Time is up, stop the timer and load the Game Over scene
                timeRemaining = 0;
                timerIsRunning = false;
                LoadGameOver();
            }
        }
    }

    private void UpdateTimerUI(float currentTime)
    {
        // Format the time in minutes and seconds and update the text component
        currentTime += 1;  // To avoid showing 59.999 as last second

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void LoadGameOver()
    {
        // Load the Game Over scene (replace "GameOverScene" with your actual scene name)
        SceneManager.LoadScene("GameOver");
    }
}
