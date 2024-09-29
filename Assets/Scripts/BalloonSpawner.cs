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
        // Start by spawning one balloon
        SpawnBalloon();
    }

    private void Update()
    {
        // Increment time since the last spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Spawn new balloons if the time interval has passed and we haven't reached the max limit
        if (timeSinceLastSpawn >= spawnInterval && currentBalloonCount < maxBalloonsOnScreen)
        {
            SpawnBalloon();
            timeSinceLastSpawn = 0f;  // Reset spawn timer
        }
    }

    public void SpawnBalloon()
    {
        // Calculate the Y spawn position off the top of the screen to avoid the arrow
        float spawnRangeY = mainCamera.orthographicSize; // Vertical range for the Y-axis
        
        // Offset Y position so balloons are above the arrow
        float posY = Random.Range(spawnRangeY, spawnRangeY + 2f); // Ensure they spawn higher up
        
        Vector3 spawnPosition;

        // Randomly decide if the balloon should spawn on the left or right side, outside the screen horizontally
        if (Random.Range(0, 2) == 0) // 50% chance of spawning on the left
        {
            // Left side spawn (off-screen)
            float leftSpawnX = -mainCamera.orthographicSize * mainCamera.aspect - 1f; // Off-screen on the left
            spawnPosition = new Vector3(leftSpawnX, posY, 0f);
        }
        else
        {
            // Right side spawn (off-screen)
            float rightSpawnX = mainCamera.orthographicSize * mainCamera.aspect + 1f; // Off-screen on the right
            spawnPosition = new Vector3(rightSpawnX, posY, 0f);
        }

        // Instantiate the balloon
        if (!(spawnPosition.y < -4f))
        {
            GameObject balloon = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
            AssignRandomMovement(balloon);

            // Track the balloon count
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
