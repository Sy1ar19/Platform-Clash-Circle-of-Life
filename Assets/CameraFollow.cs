using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform fightCameraPosition;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed = 5;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private FightTrigger _fightTrigger;

    private bool _isFightStarted = false;
    private bool _isMovingCamera = false;
    private Coroutine moveCoroutine;

    private void LateUpdate()
    {
        if (playerTransform != null && !_isFightStarted && !_isMovingCamera)
        {
            Vector3 targetPosition = playerTransform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _fightTrigger.FightStarted += OnFightStarted;
    }

    private void OnDisable()
    {
        _fightTrigger.FightStarted -= OnFightStarted;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
    }

    private void OnFightStarted()
    {
        _isFightStarted = true;
        ChangeFightCameraPosition();
    }

    private void ChangeFightCameraPosition()
    {
        if (fightCameraPosition != null)
        {
            Vector3 targetPosition = fightCameraPosition.position + new Vector3(0f, 10f, 0f); // Change 10f to the desired height
            Quaternion targetRotation = fightCameraPosition.rotation;

            SmoothMoveCamera(targetPosition, targetRotation);
        }
    }

    private void SmoothMoveCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        if (_isMovingCamera)
            return;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveCameraCoroutine(targetPosition, targetRotation));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 targetPosition, Quaternion targetRotation)
    {
        _isMovingCamera = true;
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < followSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / followSpeed);
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
        _isMovingCamera = false;
    }
}