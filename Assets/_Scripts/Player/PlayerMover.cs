using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour , IMovable
{
    [SerializeField] private float _movementSpeed;
    private Rigidbody _rigidbody;
    private bool _canMove = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
    }

    public void StopMove()
    {
        _canMove = false;
    }

    public void Move()
    {
        Vector3 forwardDirection = transform.forward * _movementSpeed;
        _rigidbody.velocity = forwardDirection * _movementSpeed;
    }
}
