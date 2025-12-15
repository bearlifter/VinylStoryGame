using UnityEngine;

// Clase esencial para el algoritmo A* con todas las propiedades requeridas
public class Node
{
    // --- PROPIEDADES DE LA CUADRÍCULA (Requeridas por GridAStar.cs) ---

    // Indica si el nodo puede ser atravesado
    public bool walkable;

    // Posición del mundo (centro del nodo)
    public Vector3 worldPosition;

    // Índices de la posición del nodo dentro del array 2D de la cuadrícula
    public int gridX;
    public int gridY;

    // --- PROPIEDADES DE COSTE DE A* (Requeridas por AStarPathfinding.cs) ---

    public int gCost; // Costo desde el nodo inicial
    public int hCost; // Costo heurístico hasta el nodo final
    public Node parent; // Nodo anterior en el camino

    // Propiedad calculada: fCost = gCost + hCost
    public int fCost
    {
        get { return gCost + hCost; }
    }

    // Constructor que toma 4 argumentos (Requerido por GridAStar.cs:45)
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    // Constructor vacío (Requerido por NPCMovement.cs)
    public Node()
    {
        // Constructor vacío para permitir crear un nodo solo con la posición,
        // como se hace en NPCMovement para el destino final.
    }
}