using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHandler : MonoBehaviour
{
    [SerializeField] private int _subdivisions = 32;
    [SerializeField] private LayerMask _whatIsObstacle;
    private LineRenderer _lineRenderer;
    private PlayerDetection _playerDetection;

    private void OnEnable()
    {
        StartCoroutine(ViewInterval());
    }

    void Awake()
    {

        _lineRenderer = GetComponentInChildren<LineRenderer>();

        _playerDetection = GetComponent<PlayerDetection>();
    }

    public void EvaluateConeOfView(int subdivisions)
    {
        _lineRenderer.positionCount = subdivisions + 1;
        float startAngle = -_playerDetection.ViewAngle;

        Vector3 lineOrigin = transform.position;
        Vector3 raycastOrigin = transform.position + new Vector3(0f, 0.5f, 0f);
        Vector3 forward = transform.forward;

        _lineRenderer.SetPosition(0, lineOrigin);

        float deltaAngle = (2 * _playerDetection.ViewAngle / subdivisions);

        for (int i = 0; i < subdivisions; i++)
        {
            float currentAngle = startAngle + deltaAngle * i;
            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * forward;
            Vector3 point = lineOrigin + direction * _playerDetection.SightDistance;

            if (Physics.Raycast(raycastOrigin, direction, out RaycastHit hit, _playerDetection.SightDistance, _whatIsObstacle))
            {
                point = hit.point;
            }
            _lineRenderer.SetPosition(i + 1, point);

        }
    }
    private IEnumerator ViewInterval()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            EvaluateConeOfView(_subdivisions);
        }
    }
}
