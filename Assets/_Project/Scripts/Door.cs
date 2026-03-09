using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animation;
    private NavMeshSurface _surface;

    public void Start()
    {
        _animation = GetComponentInChildren<Animator>();
        _surface = GetComponentInParent<NavMeshSurface>();
    }

    public void OpenDoor()
    {
        _animation.SetTrigger("Open");
        Invoke(nameof(UpdateNavMesh), 2.1f);
    }

    public void CloseDoor()
    {
        _animation.SetTrigger("Close");
        Invoke(nameof(UpdateNavMesh), 2.1f);
    }

    public void UpdateNavMesh()
    {
        _surface.BuildNavMesh();
        Debug.Log("Mesh updated");
    }
}
