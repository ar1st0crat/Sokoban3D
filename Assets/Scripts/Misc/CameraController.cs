using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject mover;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - mover.transform.position;
    }

    void LateUpdate()
    {
        transform.position = mover.transform.position + _offset;
    }
}
