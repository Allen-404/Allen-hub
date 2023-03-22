using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector2 _moveDirection;
    public float speed = 5;
    Rigidbody _rb;

    bool _upKey;
    bool _downKey;
    bool _rightKey;
    bool _leftKey;

    public Transform rotatePart;
    Animator _animator;
    PlayerInteract _playerInteract;

    void Start()
    {
        _moveDirection = Vector2.zero;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _playerInteract = GetComponent<PlayerInteract>();

        CancelInput();
    }

    void CancelInput()
    {
        _upKey = false;
        _downKey = false;
        _leftKey = false;
        _rightKey = false;
    }

    void Update()
    {
        _moveDirection = Vector2.zero;

        if (_playerInteract.isInteracting)
        {
            CancelInput();
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
            _upKey = true;
        if (Input.GetKeyUp(KeyCode.W))
            _upKey = false;
        if (Input.GetKeyDown(KeyCode.S))
            _downKey = true;
        if (Input.GetKeyUp(KeyCode.S))
            _downKey = false;
        if (Input.GetKeyDown(KeyCode.A))
            _leftKey = true;
        if (Input.GetKeyUp(KeyCode.A))
            _leftKey = false;
        if (Input.GetKeyDown(KeyCode.D))
            _rightKey = true;
        if (Input.GetKeyUp(KeyCode.D))
            _rightKey = false;


        if (_upKey)
            _moveDirection.y += 1;
        if (_downKey)
            _moveDirection.y -= 1;
        if (_rightKey)
            _moveDirection.x += 1;
        if (_leftKey)
            _moveDirection.x -= 1;
    }

    private void FixedUpdate()
    {
        var dir = _moveDirection.normalized * speed;
        var finalDir = new Vector3(dir.x, _rb.velocity.y, dir.y);
        _rb.velocity = finalDir;

        if (finalDir.magnitude > 0.01f)
        {
            rotatePart.forward = Vector3.Lerp(rotatePart.forward, finalDir, 0.1f);
            _animator.SetBool("walking", true);
        }
        else
        {
            _animator.SetBool("walking", false);
        }
    }
}