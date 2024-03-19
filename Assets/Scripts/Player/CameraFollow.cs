using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothness = 0.25f;
    private Vector3 velocity = Vector3.zero;
    private float cameraHalfSize;

    [SerializeField] private Transform target;
    [SerializeField] private float offsetY = 3f;

    [Header("Borders")]
    [SerializeField] private Transform min;
    [SerializeField] private Transform max;

    void Start() {
        Camera cam = transform.GetComponent<Camera>();

        cameraHalfSize = cam.orthographicSize * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset + new Vector3(0f, offsetY, 0f);

        if (targetPosition.x - cameraHalfSize < min.position.x) {
            Debug.Log("Hit Min border");
            targetPosition.x = min.position.x + cameraHalfSize;
        }
        else if (targetPosition.x + cameraHalfSize > max.position.x) {
            targetPosition.x = max.position.x - cameraHalfSize;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity,  smoothness);
    }
}
