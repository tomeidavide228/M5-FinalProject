using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaypointPaths
{
    public Transform[] Waypoints;
}

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private WaypointPaths[] _pathList;

    public WaypointPaths GetWaypoints(int pathIndex)
    {
        if (pathIndex >= 0 && pathIndex < _pathList.Length)
        {
        return _pathList[pathIndex];
        }
        else
        {
            Debug.Log("Path Index out of bounds");
        }
        return null;
    }
}
