using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagmovement : MovementBase
{
    [Header("Zig-Zag Movement Settings")]
    public float speed = 5f; 
    public float zigzagFrequency = 1f; 
    public float zigzagAmplitude = 1f; 

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

        // Zigzag movement
        float xMovement = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        transform.position += new Vector3(xMovement, 0, 0) * Time.deltaTime;

        
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
