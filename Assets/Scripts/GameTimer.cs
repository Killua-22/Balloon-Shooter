using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f; 
    public TextMeshProUGUI timerText; 

    private bool timerIsRunning = true;

    private void Update()
    {
       
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI(timeRemaining);
            }
            else
            {
               
                timeRemaining = 0;
                timerIsRunning = false;
                LoadGameOver();
            }
        }
    }

    private void UpdateTimerUI(float currentTime)
    {
        
        currentTime += 1;  

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void LoadGameOver()
    {
       
        SceneManager.LoadScene("GameOver");
    }
}
