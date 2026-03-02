using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public Camera mainCamera;

    [Header("Settings")]
    public float speed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10);

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float flippedMinY;
    public float flippedMaxY;
    public bool isFlipped;

    // Camera
    private float height;
    private float width;

    public void Awake()
    {
        height = mainCamera.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    public void UpdateBounds()
    {
        CameraBounds bounds = FindFirstObjectByType<CameraBounds>();

        if (bounds != null)
        {
            minX = bounds.minX;
            maxX = bounds.maxX;
            minY = bounds.minY;
            maxY = bounds.maxY;
            flippedMinY = bounds.flippedMinY;
            flippedMaxY = bounds.flippedMaxY;
        }
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        width = mainCamera.orthographicSize * mainCamera.aspect;
        height = mainCamera.orthographicSize;

        float x = 0;
        float y = 0;
        if (isFlipped)
        {
            x = Mathf.Clamp(desiredPosition.x, minX + width, maxX - width);
            y = Mathf.Clamp(desiredPosition.y, flippedMinY + height, flippedMaxY - height);
        }
        else
        {
            x = Mathf.Clamp(desiredPosition.x, minX + width, maxX - width);
            y = Mathf.Clamp(desiredPosition.y, minY + height, maxY - height);
        }

        Vector3 smoothPos = Vector3.Lerp(transform.position, new Vector3(x, y, offset.z), speed * Time.deltaTime);
        transform.position = smoothPos;
    }
}