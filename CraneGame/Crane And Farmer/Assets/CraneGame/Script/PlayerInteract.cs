using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour
{
    Animator _animator;
    public float pickUpDuration;
    public float operateWellDuration;

    public bool isInteracting { get; private set; }
    float _interactEndTime;

    public KeyCode pickupKey = KeyCode.E;
    public KeyCode wellKey = KeyCode.R;
    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteracting && Time.time >= _interactEndTime)
        {
            isInteracting = false;
        }

        if (!isInteracting)
        {
            if (Input.GetKeyDown(pickupKey))
                Pickup();
            if (Input.GetKeyDown(wellKey))
                OperateWell();
        }
    }

    void Pickup()
    {
        isInteracting = true;
        _animator.SetTrigger("pickup");
        _animator.SetBool("walking", false);
        _animator.SetBool("expelling", false);
        _interactEndTime = Time.time + pickUpDuration;
    }

    void OperateWell()
    {
        isInteracting = true;
        _animator.SetTrigger("well");
        _animator.SetBool("walking", false);
        _animator.SetBool("expelling", false);
        _interactEndTime = Time.time + operateWellDuration;
    }
}