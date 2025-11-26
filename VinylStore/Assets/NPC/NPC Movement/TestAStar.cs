using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public NPCMovement npc;
    public Vector2 targetPosition = new Vector2(3, 3);

    void Start()
    {
        if (npc == null)
        {
            Debug.LogError("No se asignó NPCMovement.");
            return;
        }

        if (npc.pathfinding == null)
        {
            Debug.LogError("NPCMovement no tiene asignado un AStarPathfinding.");
            return;
        }

        npc.MoveTo(targetPosition);
    }
}
