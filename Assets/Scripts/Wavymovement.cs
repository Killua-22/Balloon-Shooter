using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavymovement : MovementBase
{
    [Header("Wavy Movement Settings")]
    public float speed = 5f; 
    public float amplitude = 2f; 
    public float frequency = 1f; 
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        
        transform.position = new Vector3(
            Random.Range(-mainCamera.orthographicSize * mainCamera.aspect - 1f, -1f), 
            Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize),
            0f);
    }

    private void Update()
    {
        // Move horizontally
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Wavy horizontal movement
        float yMovement = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position += new Vector3(0, yMovement, 0) * Time.deltaTime;

        
        if (transform.position.x > mainCamera.orthographicSize * mainCamera.aspect + 1f)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Reset position to just off the left side of the screen
        transform.position = new Vector3(
            Random.Range(-mainCamera.orthographicSize * mainCamera.aspect - 1f, -1f), 
            Random.Range(-mainCamera.orthographicSize, mainCamera.orthographicSize), 
            0f);
    }
}
