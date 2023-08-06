using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Boss : Enemy
{
    [SerializeField] private float _raycastDistance = 60f;
    [SerializeField] private Color _rayColor = Color.red;
    [SerializeField] private float _attackDelay = 1.0f;
    [SerializeField] private Transform _shootPoint;
    
    private Player _target;
    private bool _isAttacking = false;
    private Coroutine _attackCoroutine;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(_shootPoint.position, transform.forward, out hit, _raycastDistance))
        {
            Debug.DrawRay(_shootPoint.position, transform.forward * _raycastDistance, _rayColor);

            _target = hit.collider.GetComponent<Player>();

            if (_target != null)
            {
                Debug.Log("Player detected!");
                if (_target.CurrentHealth <= 0)
                {
                    _target = null;
                }
            }
            else
            {
                _target = null;
            }
        }
        else
        {
            Debug.DrawRay(_shootPoint.position, transform.forward * _raycastDistance, Color.green);
            _target = null;
        }

        if (_target != null && !_isAttacking)
        {
            _attackCoroutine = StartCoroutine(AttackWithDelay(_target, _damage, _attackDelay));
            _animator.SetBool("IsAttacking", true);
        }
    }

    private IEnumerator AttackWithDelay(Player target, float damage, float delay)
    {
        _isAttacking = true;

        yield return new WaitForSeconds(delay);

        target.ApplyDamage(damage + UnityEngine.Random.Range(-1, 1));

        _animator.SetBool("IsAttacking", false);

        _isAttacking = false;
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
    }
}