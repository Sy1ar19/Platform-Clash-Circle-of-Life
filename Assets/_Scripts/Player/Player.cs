using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IMovable
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        Move();
    }
    public void Move()
    {
        Vector3 forwardDirection = transform.forward * _movementSpeed;
        _rigidbody.AddForce(forwardDirection);
    }

    public void Die()
    {
        Debug.LogError("Die");
    }
}
