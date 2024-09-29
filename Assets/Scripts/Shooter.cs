using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Arrow Settings")]
    [Tooltip("Arrow prefab to instantiate.")]
    public GameObject arrowPrefab;

    [Tooltip("Starting position of the arrow.")]
    public Transform arrowStartPoint;

    [Tooltip("Speed at which the arrow moves.")]
    public float arrowSpeed = 10f;

    private GameObject currentArrow;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        SpawnArrow();
    }

    private void Update()
    {
        bool ismove = arrowPrefab.GetComponent<Arrow>().isMoving;
        // Detect mouse click or screen tap
        if (Input.GetMouseButtonDown(0) && !ismove)
        {
            Vector3 targetPos = GetWorldPosition();
            ShootArrow(targetPos);
        }

        // Check if the arrow has moved off-screen and respawn if necessary
        if (currentArrow != null)
        {
            Arrow arrowScript = currentArrow.GetComponent<Arrow>();
            if (arrowScript != null && !arrowScript.isMoving)
            {
                RespawnArrow();
            }
        }
    }

    /// <summary>
    /// Spawns the arrow at the starting position.
    /// </summary>
    private void SpawnArrow()
    {
        currentArrow = Instantiate(arrowPrefab, arrowStartPoint.position, Quaternion.identity);
        Arrow arrowScript = currentArrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.speed = arrowSpeed;
        }
    }

    /// <summary>
    /// Shoots the arrow toward the target position.
    /// </summary>
    /// <param name="targetPosition">The position to shoot the arrow toward.</param>
    private void ShootArrow(Vector3 targetPosition)
    {
        if (currentArrow == null)
        {
            SpawnArrow();
        }
        
        
        Arrow arrowScript = currentArrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.SetTarget(targetPosition);
        }
        
        
    }

    /// <summary>
    /// Converts the mouse position to world position.
    /// </summary>
    /// <returns>World position corresponding to the mouse click/tap.</returns>
    private Vector3 GetWorldPosition()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z; // Set z to the distance between camera and scene

        return mainCamera.ScreenToWorldPoint(mousePos);

    }

    /// <summary>
    /// Respawns the arrow back to the starting position.
    /// </summary>
    private void RespawnArrow()
    {
        Destroy(currentArrow);
        SpawnArrow();
    }



}
