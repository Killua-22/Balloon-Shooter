using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Straightmovement : MovementBase
{
    [Header("Movement Settings")]
    public float speed = 5f; // Speed of the balloon movement
    private int direction; // -1 for left, 1 for right

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Randomly choose to start on the left or right side of the screen
        if (Random.Range(0, 2) == 0)
        {
            direction = 1; // Move right
            transform.position = new Vector3(-mainCamera.orthographicSize * mainCamera.aspect - 1f, Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize), 0f);
        }
        else
        {
            direction = -1; // Move left
            transform.position = new Vector3(mainCamera.orthographicSize * mainCamera.aspect + 1f, Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize), 0f);
        }
    }

    private void Update()
    {
        // Move horizontally based on direction
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);

        // Check if the balloon has moved off the screen
        if (Mathf.Abs(transform.position.x) > mainCamera.orthographicSize * mainCamera.aspect + 1f)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Reset position to the opposite side of the screen
        if (direction == 1)
        {
            transform.position = new Vector3(-mainCamera.orthographicSize * mainCamera.aspect - 1f, Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize), 0f);
        }
        else
        {
            transform.position = new Vector3(mainCamera.orthographicSize * mainCamera.aspect + 1f, Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize), 0f);
        }
    }
}
