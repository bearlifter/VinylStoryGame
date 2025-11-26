using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    GridAStar grid;

    void Awake()
    {
        grid = GetComponent<GridAStar>();
    }

    public List<Node> FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new();
        HashSet<Node> closedSet = new();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node current = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
                if (openSet[i].fCost < current.fCost ||
                    openSet[i].fCost == current.fCost && openSet[i].hCost < current.hCost)
                    current = openSet[i];

            openSet.Remove(current);
            closedSet.Add(current);

            if (current == targetNode)
                return RetracePath(startNode, targetNode);

            foreach (Node neighbour in grid.GetNeighbours(current))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                int newCost = current.gCost + GetDistance(current, neighbour);
                if (newCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCost;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = current;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        return null;
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new();
        Node current = endNode;

        while (current != startNode)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }

    int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);
        return 14 * Mathf.Min(dstX, dstY) + 10 * Mathf.Abs(dstX - dstY);
    }
}
