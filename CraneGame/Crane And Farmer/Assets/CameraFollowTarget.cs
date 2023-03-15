using UnityEngine;
using System.Collections;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target;

    Vector3 _offset;

    void Start()
    {
        _offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + _offset;
    }
}
