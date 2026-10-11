using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Drag your player here in the Inspector
    private Vector3 offset;

    void Start()
    {
        if (playerTransform != null)
        {
            offset = transform.position - playerTransform.position;
        }
    }

    void LateUpdate()
    {
        if (playerTransform == null) return;

        // Calculate the desired position using the original offset
        Vector3 targetPosition = playerTransform.position + offset;

        // Smoothly interpolate between current camera position and the target position
        transform.position = targetPosition;
    }
}