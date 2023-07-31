using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Player : MonoBehaviour, IMovable
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody _rigidbody;
    private bool _canMove = true;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
    }
    public void Move()
    {
        Vector3 forwardDirection = transform.forward * _movementSpeed;
        //_rigidbody.MovePosition(_rigidbody.position + forwardDirection * _movementSpeed * Time.deltaTime);
        _rigidbody.velocity = forwardDirection * _movementSpeed;
        //_rigidbody.AddForce(forwardDirection);
    }

    public void Die()
    {
        Debug.LogError("Die");
    }

    public void StopMove()
    {
        SetIdleAnimation();
        _canMove = false;
    }

    private void SetMovingAnimation()
    {
        _animator.SetBool("IsMoving", true);
    }

    private void SetIdleAnimation()
    {
        _animator.SetBool("IsIdle", true);
    }
}
