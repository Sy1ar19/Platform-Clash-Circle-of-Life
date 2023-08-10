using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform _fightCameraPosition;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _followSpeed = 5;
    [SerializeField] private float _rotationSpeed = 5;
    [SerializeField] private FightTrigger _fightTrigger;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerHealth _playerHealth;

    private bool _isFightStarted = false;
    private bool _isMovingCamera = false;
    private Coroutine _moveCoroutine;

    private void LateUpdate()
    {
        if (_player != null && _playerHealth.IsAlive && _isFightStarted==false && _isMovingCamera==false)
        {
            Vector3 targetPosition = _player.transform.position + _offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _fightTrigger.FightStarted += OnFightStarted;
    }

    private void OnDisable()
    {
        _fightTrigger.FightStarted -= OnFightStarted;

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }

    private void OnFightStarted()
    {
        _isFightStarted = true;
        ChangeFightCameraPosition();
    }

    private void ChangeFightCameraPosition()
    {
        if (_fightCameraPosition != null)
        {
            Vector3 targetPosition = _fightCameraPosition.position + new Vector3(0f, 10f, 0f);
            Quaternion targetRotation = _fightCameraPosition.rotation;

            SmoothMoveCamera(targetPosition, targetRotation);
        }
    }

    private void SmoothMoveCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        if (_isMovingCamera)
            return;

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }

        _moveCoroutine = StartCoroutine(MoveCameraCoroutine(targetPosition, targetRotation));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 targetPosition, Quaternion targetRotation)
    {
        _isMovingCamera = true;
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < _followSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _followSpeed);
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
        _isMovingCamera = false;
    }
}