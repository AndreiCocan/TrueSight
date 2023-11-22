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


            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(transform.position, out closestHit, 500, NavMesh.AllAreas))
            {


                Vector3 nearestNavMeshPosition = closestHit.position;

                NavMesh.CalculatePath(nearestNavMeshPosition, target.position, NavMesh.AllAreas, path);


                VibrationFromPathDeviation();

            }
            else
            {
                throw new System.Exception("No NavMesh Found");
            }

        }
        DrawPath();
    }

    private void VibrationFromPathDeviation()
    {
        if(path.corners.Length >= 2)
        {
            Vector3 PathDirectionProj = Vector3.ProjectOnPlane(path.corners[1] - path.corners[0], Vector3.up);

            float DeviationAngle = Vector3.SignedAngle(PathDirectionProj, transform.forward, Vector3.up);
            vm.directionValue = DeviationAngle;
        }

    }

    private void DrawPath()
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }

    }

}
