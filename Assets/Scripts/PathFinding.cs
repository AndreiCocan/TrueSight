using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    public Transform target;
    private NavMeshPath path;

    [SerializeField]
    public VibrationManager_minimal vm;

    private float elapsed = 0.0f;
    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
    }

    void Update()
    {
        PathCalc();
    }

    private void PathCalc()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 0.1f)
        {
            elapsed -= 0.1f;
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            DeviationFromPathCalc();
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }

    private void DeviationFromPathCalc()
    {
        Vector3 PathDirection = path.corners[1] - path.corners[0];
        float DeviationAngle = Vector3.SignedAngle(PathDirection, transform.forward,Vector3.up);
        vm.directionValue = DeviationAngle;

    }

}
