using UnityEngine;

public class PrarallaxBackgroundScript : MonoBehaviour
{
    private Vector3 previousCameraPosition;

    public Transform cameraTransform;

    public float parallaxFactorX = 0.5f;

    public float parallaxFactorY = 0.5f;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        previousCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        Vector3 delta = cameraTransform.position - previousCameraPosition;
        transform.position += new Vector3(delta.x * parallaxFactorX, delta.y * parallaxFactorY, 0);
        previousCameraPosition = cameraTransform.position;
    }
}
