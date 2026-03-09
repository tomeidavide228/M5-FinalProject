using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrol,
    Rotate,
    Chase
}

public class EnemyAgent : MonoBehaviour
{
    [Header("Enemy States Settings")]
    [SerializeField] private WaypointManager _waypointsManager;
    [SerializeField] private EnemyState _defaultState;
    [SerializeField] private int pathIndex;

    [Header("Patrol Settings")]

    [SerializeField] private float _waitTime = 3f;


    [Header("Rotate Settings")]

    [SerializeField] private int rotationSpeed;
    [SerializeField] private float _rotationDuration = 1f;
    [SerializeField] private float _rotationInterval = 1f;
    [SerializeField] private float _loseSightTime = 1f;

    [Header("Chase Settings")]

    [SerializeField] private float _chaseInterval;

    private EnemyState _currentState;
    private NavMeshAgent _agent;
    private PlayerDetection _sight;
    private WaypointPaths _waypoints;
    private int currentIndex = 0;
    bool _isWaiting;
    private Coroutine _waitCoroutine;
    private Coroutine _rotateCoroutine;
    private float _lastChase;
    private float _lastTimeSeenPlayer;
    private Vector3 _returnPosition;
    private float _rotation = 90f;
    private bool _isRotating = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _sight = GetComponent<PlayerDetection>();
    }

    private void Start()
    {
        _currentState = _defaultState;
        _waypoints = _waypointsManager.GetWaypoints(pathIndex);
        _agent.SetDestination(_waypoints.Waypoints[currentIndex].position);

    }

    private void Update()
    {
        if (_sight.CanSeePlayer())
        {
            _lastTimeSeenPlayer = Time.time;
        }

        EnemyState newState;

        if (Time.time - _lastTimeSeenPlayer < _loseSightTime)
        {
            newState = EnemyState.Chase;
        }
        else
        {
            if (_currentState == EnemyState.Rotate)
            {
                _agent.isStopped = false;
                _agent.updateRotation = true;
                _agent.SetDestination(_returnPosition);
            }

            newState = _defaultState;
        }

        if (newState != _currentState)
        {
            ChangeState(newState);
        }

        switch (_currentState)
        {
            case EnemyState.Patrol:
                Patrolling();
                break;
            case EnemyState.Rotate:
                Rotating();
                break;
            case EnemyState.Chase:
                Chasing();
                break;
        }
    }

    private void Patrolling()
    {
        if (_isWaiting)
        {
            return;
        }
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _waitCoroutine = StartCoroutine(WaitInterval());
        }
    }

    private void Rotating()
    {
        if (!_isRotating)
        {
            StartCoroutine(Rotate());
        }
    }

    private void Chasing()
    {
        if (Time.time - _lastChase > _chaseInterval)
        {
            _agent.SetDestination(_sight.Player.position);
            _lastChase = Time.time;
        }
    }


    IEnumerator WaitInterval()
    {
        _isWaiting = true;
        _agent.isStopped = true;
        yield return new WaitForSeconds(_waitTime);

        currentIndex = (currentIndex + 1) % _waypoints.Waypoints.Length;
        _agent.SetDestination(_waypoints.Waypoints[currentIndex].position);

        _agent.isStopped = false;
        _isWaiting = false;
        _waitCoroutine = null;
    }
    private IEnumerator Rotate()
    {
        _isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, _rotation, 0);

        float elapsedTime = 0f;

        while (elapsedTime < _rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / _rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        yield return new WaitForSeconds(_rotationInterval);

        _isRotating = false;
    }
    private void ChangeState(EnemyState newState)
    {
        _currentState = newState;
        OnEnterState(newState);
    }
    private void OnEnterState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Patrol:
                _agent.isStopped = false;
                _agent.updateRotation = true;
                break;
            case EnemyState.Rotate:
                _agent.isStopped = true;
                _agent.updateRotation = false;
                break;
            case EnemyState.Chase:
                SetupChase();
                break;
        }
    }

    private void SetupChase()
    {
        _agent.updateRotation = true;
        _agent.isStopped = false;

        _returnPosition = _waypoints.Waypoints[currentIndex].position;

        if (_rotateCoroutine != null)
        {
            StopCoroutine(_rotateCoroutine);
            _rotateCoroutine = null;
            _isRotating = false;
        }

        if (_waitCoroutine != null)
        {
            StopCoroutine(_waitCoroutine);
            _waitCoroutine = null;
            _isWaiting = false;
        }

        _lastChase = 0f;
    }

}