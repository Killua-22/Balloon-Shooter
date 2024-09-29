using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [Header("Balloon Settings")]
    public int maxBalloonsOnScreen = 6;  // Maximum number of balloons allowed on screen at once
    public GameObject balloonPrefab;
    public float spawnInterval = 1f;  // Time between balloon spawns

    private Camera mainCamera;
    private int currentBalloonCount = 0;
    private float timeSinceLastSpawn = 0f;
    private List<GameObject> activeBalloons = new List<GameObject>();

    private void Start()
    {
        mainCamera = Camera.main;
        
        SpawnBalloon();
    }

    private void Update()
    {
        
        timeSinceLastSpawn += Time.deltaTime;

        
        if (timeSinceLastSpawn >= spawnInterval && currentBalloonCount < maxBalloonsOnScreen)
        {
            SpawnBalloon();
            timeSinceLastSpawn = 0f;  // Reset spawn timer
        }
    }

    public void SpawnBalloon()
    {
        
        float spawnRangeY = mainCamera.orthographicSize; 
        
        
        float posY = Random.Range(spawnRangeY, spawnRangeY + 2f); 
        
        Vector3 spawnPosition;

        if (Random.Range(0, 2) == 0) 
        {
            // Left side spawn (off-screen)
            float leftSpawnX = -mainCamera.orthographicSize * mainCamera.aspect - 1f; 
            spawnPosition = new Vector3(leftSpawnX, posY, 0f);
        }
        else
        {
            // Right side spawn (off-screen)
            float rightSpawnX = mainCamera.orthographicSize * mainCamera.aspect + 1f; 
            spawnPosition = new Vector3(rightSpawnX, posY, 0f);
        }

        // Instantiate the balloon
        if (!(spawnPosition.y < -4f))
        {
            GameObject balloon = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
            AssignRandomMovement(balloon);

            
            currentBalloonCount++;
            activeBalloons.Add(balloon);

            // Listen for when the balloon gets destroyed or popped
            Balloon balloonComponent = balloon.GetComponent<Balloon>();
            if (balloonComponent != null)
            {
                balloonComponent.OnBalloonPopped += OnBalloonPopped;
            }
        }
        

        
    }

    // This gets called when a balloon is popped/destroyed
    private void OnBalloonPopped(GameObject balloon)
    {
        // Decrease the balloon count and remove it from the active balloons list
        currentBalloonCount--;
        activeBalloons.Remove(balloon);
    }

    private void AssignRandomMovement(GameObject balloon)
    {
        int pattern = Random.Range(0, 3); // 0: Straight, 1: Wavy, 2: Zig-Zag

        switch (pattern)
        {
            case 0:
                balloon.AddComponent<Straightmovement>();
                break;
            case 1:
                balloon.AddComponent<Wavymovement>();
                break;
            case 2:
                balloon.AddComponent<ZigZagmovement>();
                break;
        }
    }
}
