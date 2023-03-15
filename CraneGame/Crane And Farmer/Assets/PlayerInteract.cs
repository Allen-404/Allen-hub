using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour
{
    Animator _animator;
    public float duration;
    public bool isPickingUp { get; private set; }
    float _lastPickupTime;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickingUp && Time.time >= _lastPickupTime + duration)
        {
            isPickingUp = false;
        }

        if (!isPickingUp && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        isPickingUp = true;
        _animator.SetTrigger("pickup");
        _animator.SetBool("walking", false);
        _animator.SetBool("expelling", false);
        _lastPickupTime = Time.time;
    }
}
