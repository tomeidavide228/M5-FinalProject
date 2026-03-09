using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharactersAnimation : MonoBehaviour
{
    [Header("Animations Settings")]
    [SerializeField] private string _forward = "forward";
    [SerializeField] private string _horizontal = "horizontal";

    private Animator _animation;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _animation = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 velocity = _agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float forwardSpeed = localVelocity.z;
        float horizontalSpeed = localVelocity.x;

        _animation.SetFloat(_forward, forwardSpeed);
        _animation.SetFloat(_horizontal, horizontalSpeed);
    }
}
