using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void Update()
    {
        Vector3 position = transform.position;
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, position.z);
    }
}
