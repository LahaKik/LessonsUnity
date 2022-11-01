using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Vector3 Direction;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceCheck;
    [SerializeField] private LayerMask _ignoreLayer;
    [SerializeField] private GameObject _projectileFX;


    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Direction * _speed * Time.deltaTime;

        Ray projectileRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(projectileRay, out RaycastHit hit, _distanceCheck, ~_ignoreLayer))
        {
            Destroy(gameObject);
            GameObject _FX = Instantiate(_projectileFX, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
