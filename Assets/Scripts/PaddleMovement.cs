using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    
    
    [SerializeField] private KeyCode upKey,
        downKey;
    
    // How fast the paddle can move
    [SerializeField] private float speed;


    public bool IsMovingUp { get; private set; }
    public bool IsMovingDown { get; private set; }


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }
    
    private void UpdateMovement()
    {
        if (IsMovingUp)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.up * (speed * Time.fixedDeltaTime));
        }

        if (IsMovingDown)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.down * (speed * Time.fixedDeltaTime));
        }
    }

    private void CheckInput()
    {
        IsMovingUp = Input.GetKey(upKey);
        IsMovingDown = !IsMovingUp && Input.GetKey(downKey);
    }
}
