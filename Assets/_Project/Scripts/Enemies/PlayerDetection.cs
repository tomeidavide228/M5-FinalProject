using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Sight Settings")]

    [SerializeField] private Transform _head;
    [SerializeField] private float _viewAngle = 45f;
    [SerializeField] private float _sightDistance = 10f;
    [SerializeField] private LayerMask _whatIsObstacle;
    [SerializeField] private Transform _player;

    public Transform Player => _player;
    public float ViewAngle => _viewAngle;
    public float SightDistance => _sightDistance;

    void Update()
    {
        if (Player != null && CanSeePlayer())
        {
            Debug.Log("The player was seen");
        }
    }
public bool CanSeePlayer()
{
    Vector3 toTarget = Player.position - transform.position;
    float sqrDistance = toTarget.sqrMagnitude;

    if (sqrDistance > _sightDistance * _sightDistance)
        return false;

    float distance = Mathf.Sqrt(sqrDistance);
    Vector3 direction = toTarget / distance;

    if (Vector3.Dot(transform.forward, direction) < Mathf.Cos(_viewAngle * Mathf.Deg2Rad))
        return false;

    if (Physics.Raycast(_head.position, direction, out RaycastHit hit, _sightDistance, _whatIsObstacle))
    {
        if (hit.transform != Player)
            return false;
    }

    return true;
}

}