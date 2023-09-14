using UnityEngine;

public class Circle : MonoBehaviour, IMovable
{
    [SerializeField] private float _rotationSpeed = 1000f;
    [SerializeField] private float _raycastDistance = 100f;
    [SerializeField] private LayerMask _fightTriggerMask;

    private float _minStartingRotation = 0f;
    private float _maxStartingRotation = 360f;

    private bool _isRotating = false;
    private bool _canRotate = true;
    private Transform _selectedCircle;
    private Vector2 _startTouchPosition;
    private Quaternion _startRotation;

    private void Start()
    {
        SetRandomStartingRotation();
    }

    private void Update()
    {
        if (_canRotate)
        {
            Move();
        }
    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _raycastDistance, _fightTriggerMask))
            {
                if (hit.collider.TryGetComponent<DeahtTrigger>(out DeahtTrigger deahtTrigger))
                {
                    _selectedCircle = deahtTrigger.transform.parent;
                    _isRotating = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isRotating = false;
        }

        if (_isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X");
            _selectedCircle.transform.Rotate(Vector3.up, mouseX * _rotationSpeed * Time.deltaTime);
        }
    }

    /*    public void Move()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, _raycastDistance, _fightTriggerMask))
                {
                    if (hit.collider.TryGetComponent<DeahtTrigger>(out DeahtTrigger deahtTrigger))
                    {
                        _selectedCircle = deahtTrigger.transform.parent;
                        _isRotating = true;
                        _startTouchPosition = Input.mousePosition;
                        _startRotation = _selectedCircle.rotation; // Сохраняем текущее вращение
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isRotating = false;
            }

            if (_isRotating)
            {
                Vector2 currentTouchPosition = Input.mousePosition;
                Vector2 deltaPosition = currentTouchPosition - _startTouchPosition;
                float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;

                // Применяем угол к текущему вращению, используя сохраненное начальное вращение
                _selectedCircle.rotation = _startRotation * Quaternion.Euler(0f, -angle, 0f);
            }
        }*/

    private void SetRandomStartingRotation()
    {
        float randomRotation = Random.Range(_minStartingRotation, _maxStartingRotation);
        transform.Rotate(Vector3.up, randomRotation);
    }
}
