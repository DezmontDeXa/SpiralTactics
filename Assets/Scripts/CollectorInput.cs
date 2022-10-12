using UnityEngine;
using UnityEngine.EventSystems;

public class CollectorInput : MonoBehaviour
{
    [SerializeField] private CollectorMovement collectorMovement;

    private Vector3 _targetPosition;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Click();
    }

    private void Click()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
            _targetPosition = transform.position;
        }
        collectorMovement.transform.LookAt(transform);
        collectorMovement.RunToPoint(_targetPosition);

    }
}
