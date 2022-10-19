using UnityEngine;

public class ShellMove : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.position += transform.forward * speed * com.GameTime.deltaTime;
    }
}
