using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);

    /*
    * Set the camera to follow the target
    */
    public void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
