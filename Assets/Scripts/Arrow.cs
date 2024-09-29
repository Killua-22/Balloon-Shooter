using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Arrow Settings")]
    [Tooltip("Speed at which the arrow moves.")]
    public float speed = 10f;

    private Vector3 targetPosition;
    public bool isMoving = false;

    /// <summary>
    /// Sets the target position for the arrow to move toward.
    /// </summary>
    /// <param name="target">The world position to move toward.</param>
    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            // Calculate direction towards target
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Move the arrow
            transform.Translate(direction * speed * Time.deltaTime);

            // Check if the arrow has reached or passed the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Respawn();
            }

            // Alternatively, check if the arrow has moved off-screen
            if (IsOffScreen())
            {
                Respawn();
            }
        }
    }

    /// <summary>
    /// Respawns the arrow back to the starting position.
    /// </summary>
    private void Respawn()
    {
        transform.position = Vector3.zero; // Adjust based on your starting point
        isMoving = false;
        gameObject.SetActive(true);
        /*Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Re-enable physics for the next shot*/
    }

    /// <summary>
    /// Checks if the arrow has moved off-screen.
    /// </summary>
    /// <returns>True if off-screen; otherwise, false.</returns>
    private bool IsOffScreen()
    {
        Camera mainCamera = Camera.main;
        Vector3 pos = transform.position;
        float screenLeft = -mainCamera.orthographicSize * mainCamera.aspect;
        float screenRight = mainCamera.orthographicSize * mainCamera.aspect;
        float screenTop = mainCamera.orthographicSize;
        float screenBottom = -mainCamera.orthographicSize;

        return (pos.x < screenLeft - 1f || pos.x > screenRight + 1f ||
                pos.y < screenBottom - 1f || pos.y > screenTop + 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Balloon"))
        {

            /*Rigidbody rb = GetComponent<Rigidbody>();

            // Check if Rigidbody is present
            if (rb != null)
            {
                rb.isKinematic = true;  // Stop all forces acting on the arrow
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;  // Stop any rotation
            }*/

            //DestroyArrow();  // Destroy or reset arrow
            
            gameObject.SetActive(false);
            Destroy(collision.gameObject);

            FindObjectOfType<ScoreManager>().AddScore(1);

            /*// Destroy or deactivate the balloon
            Destroy(collision.gameObject);*/
            // Respawn the arrow
            Respawn();

            FindObjectOfType<BalloonSpawner>().SpawnBalloon();
        }
    }

}
