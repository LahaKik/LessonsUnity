using Assets.Code.Infrastrucrure;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private LayerMask _ignoreLayer;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _distanceCheck;
    private AllServices _services = AllServices.Instance;


    private Vector3 direction;
    private InputService _inputService;

    void Start()
    {
        _inputService = _services.GetService<InputService>();
    }

    void Update()
    {
        if (_inputService.GetShootButton())
        {
            Ray cameraRay = GetMousePosToRay();

            if (Physics.Raycast(cameraRay, out RaycastHit hit, _distanceCheck, ~_ignoreLayer))
            {
                direction = ComputeDirection(hit);

                LaunchBullet(hit);
            }
        }
    }

    private Vector3 ComputeDirection(RaycastHit hit)
    {
        return hit.point - _gunPoint.position;
    }

    private void LaunchBullet(RaycastHit hit)
    {
        GameObject projectile = Instantiate(_projectilePrefab, _gunPoint.transform.position, Quaternion.identity);
        projectile.transform.LookAt(hit.point);
        projectile.GetComponent<Projectile>().Direction = direction.normalized;
    }

    private Ray GetMousePosToRay() 
        => Camera.main.ScreenPointToRay(_inputService.MousePosition);
}
