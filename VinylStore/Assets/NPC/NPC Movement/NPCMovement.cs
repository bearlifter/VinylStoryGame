using UnityEngine;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour
{
    public AStarPathfinding pathfinding;

    // DEBEN SER PÚBLICAS para que NPCFlow pueda leer el estado del movimiento
    public List<Node> path;
    public int pathIndex;

    public float speed = 3f;
    private Animator animator;
    private Vector2 lastMovementDirection = Vector2.down;

    void Start()
    {
        if (pathfinding == null)
            Debug.LogError("NPCMovement NO tiene asignado un AStarPathfinding.");

        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("NPCMovement NO tiene asignado un Animator.");
    }

    public void MoveTo(Vector2 target)
    {
        if (pathfinding == null)
        {
            Debug.LogError("No hay referencia a AStarPathfinding.");
            return;
        }

        // 1. Obtener el camino del A* (que va a centros de nodos)
        path = pathfinding.FindPath(transform.position, target);
        pathIndex = 0;

        // 2. CORRECCIÓN CLAVE: Añadir el punto de destino EXACTO
        if (path != null && path.Count > 0)
        {
            // Creamos un nuevo nodo (solo con la posición) que será el punto final
            Node finalNode = new Node();
            finalNode.worldPosition = target;

            // Reemplazamos el último nodo generado por A* con la posición exacta,
            // o simplemente lo añadimos al final del camino generado por A*.
            // Usamos ADD para asegurar que todos los nodos generados por A* se recorran.
            path.Add(finalNode);

            Debug.Log($"Pathfinding OK. Camino ajustado al destino exacto: {target}");
        }
        else
        {
            // Diagnóstico añadido para cuando el A* falla
            Debug.LogError($"🚨 PATHFINDING FALLÓ al intentar ir a {target}. Revisar obstáculos/Grid.");
        }
    }

    void Update()
    {
        bool isMoving = (path != null && pathIndex < path.Count);

        if (animator != null)
        {
            animator.SetBool("isWalking", isMoving);
        }

        if (!isMoving)
        {
            if (animator != null)
            {
                animator.SetFloat("LastInputX", lastMovementDirection.x);
                animator.SetFloat("LastInputY", lastMovementDirection.y);
            }
            return;
        }

        Vector2 targetPos = path[pathIndex].worldPosition;
        Vector2 oldPos = transform.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        Vector2 movementDirection = ((Vector2)transform.position - oldPos).normalized;

        if (animator != null)
        {
            animator.SetFloat("InputX", movementDirection.x);
            animator.SetFloat("InputY", movementDirection.y);
        }

        if (movementDirection.sqrMagnitude > 0.001f)
        {
            lastMovementDirection = movementDirection;
        }

        // Si estamos lo suficientemente cerca del punto actual del path, pasamos al siguiente nodo
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            pathIndex++;
    }
}