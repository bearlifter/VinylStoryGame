using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector2 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost => gCost + hCost;

    public Node(bool _walkable, Vector2 _worldPos, int x, int y)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = x;
        gridY = y;
    }
}
