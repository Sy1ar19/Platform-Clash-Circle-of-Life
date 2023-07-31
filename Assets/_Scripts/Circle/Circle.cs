using UnityEngine;

public class Circle : MonoBehaviour, IMovable
{
    [SerializeField] private float _rotationSpeed = 1000f;

    private bool _isRotating = false;
    private bool _canRotate = true;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
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

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<DeahtTrigger>(out DeahtTrigger deahtTrigger))
                {
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
            _transform.Rotate(Vector3.up, mouseX * _rotationSpeed * Time.deltaTime);
        }
    }
}
