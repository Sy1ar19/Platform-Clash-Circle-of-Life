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

    private void SetRandomStartingRotation()
    {
        float randomRotation = Random.Range(_minStartingRotation, _maxStartingRotation);
        transform.Rotate(Vector3.up, randomRotation);
    }
}
