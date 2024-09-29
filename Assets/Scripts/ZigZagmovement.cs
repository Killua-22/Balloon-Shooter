using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagmovement : MovementBase
{
    [Header("Zig-Zag Movement Settings")]
    public float speed = 5f; // Speed of the balloon
    public float zigzagFrequency = 1f; // Frequency of the zig-zag movement
    public float zigzagAmplitude = 1f; // Amplitude of the zig-zag movement

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

        // Zigzag movement
        float xMovement = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        transform.position += new Vector3(xMovement, 0, 0) * Time.deltaTime;

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
