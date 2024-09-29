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

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            
            Vector3 direction = (targetPosition - transform.position).normalized;

            
            transform.Translate(direction * speed * Time.deltaTime);

            
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Respawn();
            }

            
            if (IsOffScreen())
            {
                Respawn();
            }
        }
    }

    private void Respawn()
    {
        transform.position = Vector3.zero; // Adjust based on your starting point
        isMoving = false;
        gameObject.SetActive(true);
        /*Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Re-enable physics for the next shot*/
    }


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

            
            gameObject.SetActive(false);
            Destroy(collision.gameObject);

            FindObjectOfType<ScoreManager>().AddScore(1);

            Respawn();

            FindObjectOfType<BalloonSpawner>().SpawnBalloon();
        }
    }

}
