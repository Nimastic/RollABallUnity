using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        // Set this on Start
        offset = transform.position - player.transform.position;
    }

    // Use this for last mode states
    // Runs after all other Update()
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}