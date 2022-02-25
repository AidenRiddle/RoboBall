using UnityEngine;

public class RotateLookTargets : MonoBehaviour
{
    [SerializeField] Transform TPSCamera;

    private void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, TPSCamera.eulerAngles.y, transform.eulerAngles.z);
    }
}
