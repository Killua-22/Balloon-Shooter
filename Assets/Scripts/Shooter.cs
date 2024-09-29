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

        if (currentArrow != null)
        {
            Arrow arrowScript = currentArrow.GetComponent<Arrow>();
            if (arrowScript != null && !arrowScript.isMoving)
            {
                RespawnArrow();
            }
        }
    }

    private void SpawnArrow()
    {
        currentArrow = Instantiate(arrowPrefab, arrowStartPoint.position, Quaternion.identity);
        Arrow arrowScript = currentArrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.speed = arrowSpeed;
        }
    }

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

    private Vector3 GetWorldPosition()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z; // Set z to the distance between camera and scene

        return mainCamera.ScreenToWorldPoint(mousePos);

    }

    private void RespawnArrow()
    {
        Destroy(currentArrow);
        SpawnArrow();
    }



}
