using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _player;
    private Camera _camera;
    private Coroutine _coroutine;
    private Vector3 _playerDestination;

    private void Awake()
    {
        _player = GetComponent<NavMeshAgent>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider)
            {
                //if (_coroutine != null) StopCoroutine(_coroutine);
                //_coroutine = StartCoroutine(PlayerMove(hit.point));
                _player.SetDestination(hit.point);
                _playerDestination = hit.point;
            }
        }
    }

    //private IEnumerator PlayerMove(Vector3 point)
    //{
    //    float playerDistanceToGround = transform.position.y - point.y;
    //    point.y += playerDistanceToGround;
    //    while (Vector3.Distance(transform.position, point) > 0.1f)
    //    {
    //        _player.SetDestination(point);
    //        yield return null;
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_playerDestination, 1);
    }
}
