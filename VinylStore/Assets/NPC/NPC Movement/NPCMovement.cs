using UnityEngine;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour
{
    public AStarPathfinding pathfinding;  // <--- AHORA público y asignable
    List<Node> path;
    int pathIndex;

    public float speed = 3f;

    void Start()
    {
        if (pathfinding == null)
            Debug.LogError("NPCMovement NO tiene asignado un AStarPathfinding.");
    }

    public void MoveTo(Vector2 target)
    {
        if (pathfinding == null)
        {
            Debug.LogError("No hay referencia a AStarPathfinding.");
            return;
        }

        path = pathfinding.FindPath(transform.position, target);
        pathIndex = 0;
    }

    void Update()
    {
        if (path == null || pathIndex >= path.Count) return;

        Vector2 targetPos = path[pathIndex].worldPosition;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            pathIndex++;
    }
}
