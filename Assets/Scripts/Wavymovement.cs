using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavymovement : MovementBase
{
    [Header("Wavy Movement Settings")]
    public float speed = 5f; // Speed of the balloon
    public float amplitude = 2f; // Amplitude of the wavy movement
    public float frequency = 1f; // Frequency of the wavy movement

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Set initial position off-screen
        transform.position = new Vector3(
            Random.Range(-mainCamera.orthographicSize * mainCamera.aspect - 1f, -1f), // Left side spawn
            Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize), // Random Y position within screen height
            0f);
    }

    private void Update()
    {
        // Move horizontally
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Wavy horizontal movement
        float yMovement = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position += new Vector3(0, yMovement, 0) * Time.deltaTime;

        // Check if the balloon has moved off the right side of the screen
        if (transform.position.x > mainCamera.orthographicSize * mainCamera.aspect + 1f)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Reset position to just off the left side of the screen
        transform.position = new Vector3(
            Random.Range(-mainCamera.orthographicSize * mainCamera.aspect - 1f, -1f), // Left side spawn
            Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize), // Random Y position within screen height
            0f);
    }
}
