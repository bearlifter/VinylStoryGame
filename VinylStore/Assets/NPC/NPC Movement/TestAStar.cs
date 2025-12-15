using System.Collections;
using UnityEngine;

public class NPCFlow : MonoBehaviour
{
    [Header("Referencias")]
    public NPCMovement npcMovement;

    [Header("Posiciones y Parámetros del Flujo")]
    [Tooltip("Coordenadas exactas (X, Y) del punto visible del mostrador.")]
    public Vector2 counterPosition = new Vector2(6.50f, 0.00f);

    public float shelfInteractionDuration = 3f;
    public float idleTimeAtSpawn = 3f; // <--- Tiempo de espera en el spawn
    public int coinRewardAmount = 10;

    // Variables internas de estado
    private Vector2 initialPosition;
    private Vector2 targetShelfCoordinates;
    private bool hasClickedShelf = false;

    void Start()
    {
        if (npcMovement == null)
            npcMovement = GetComponent<NPCMovement>();

        if (npcMovement == null || GameFlow.Instance == null || CoinManager.Instance == null)
        {
            Debug.LogError("Faltan referencias clave. Asegúrate de que NPCMovement, GameFlow y CoinManager existan.");
            enabled = false;
            return;
        }

        // Guarda la posición inicial donde spawnea el NPC
        initialPosition = (Vector2)transform.position;
        StartCoroutine(RunSimpleFlowLoop());
    }

    // Método llamado por el ClickManager al clicar un estante
    public void SetTargetAndSignalClick(Vector3 coordinates)
    {
        if (GameFlow.Instance.isShelfClickable)
        {
            targetShelfCoordinates = (Vector2)coordinates;
            hasClickedShelf = true;
            GameFlow.Instance.SetShelfClickable(false);
        }
    }

    // Función de ayuda para comprobar si el NPC ha dejado de moverse
    private bool IsNPCFinishedMoving()
    {
        return !(npcMovement.path != null && npcMovement.pathIndex < npcMovement.path.Count);
    }

    IEnumerator RunSimpleFlowLoop()
    {
        while (true) // Bucle infinito
        {
            Debug.Log("--- INICIO DEL BUCLE DE COMPRA ---");
            hasClickedShelf = false;

            // Calculamos la posición segura (nodo caminable) para el mostrador
            Vector2 safeWaitPosition = counterPosition;

            // 1. MOVERSE AL MOSTRADOR (Punto Seguro)
            Debug.Log($"Estado: Moviéndose al Mostrador (Punto Seguro: {safeWaitPosition})");
            npcMovement.MoveTo(safeWaitPosition);
            yield return new WaitUntil(IsNPCFinishedMoving);

            // 2. ESPERAR CLIC EN UNO DE LOS TRES OBJETOS
            Debug.Log("Estado: Esperando clic en estante (Clics habilitados)");
            GameFlow.Instance.SetShelfClickable(true);
            yield return new WaitUntil(() => hasClickedShelf);

            // 3. MOVERSE AL ESTANTE CLICADO
            Debug.Log($"Estado: Moviéndose al estante en {targetShelfCoordinates}");
            npcMovement.MoveTo(targetShelfCoordinates);
            yield return new WaitUntil(IsNPCFinishedMoving);

            // 4. ESPERAR EN EL ESTANTE (Interacción 3s)
            Debug.Log($"Estado: Interactuando con el estante por {shelfInteractionDuration}s");
            yield return new WaitForSeconds(shelfInteractionDuration);

            // 5. VOLVER AL MOSTRADOR (Punto Seguro)
            Debug.Log("Estado: Volviendo al Mostrador para Pagar (Punto Seguro)");
            npcMovement.MoveTo(safeWaitPosition);
            yield return new WaitUntil(IsNPCFinishedMoving);

            // 6. RECOMPENSA
            Debug.Log("Estado: Procesando recompensa (+10 monedas)");
            CoinManager.Instance.AddCoins(coinRewardAmount);

            // ==========================================================
            // 7. RETORNO A POSICIÓN INICIAL Y ESPERA (NUEVA LÓGICA)
            // ==========================================================

            // Moverse al punto de spawn inicial
            Debug.Log($"Estado: Volviendo a la Posición Inicial de Spawn: {initialPosition}");
            npcMovement.MoveTo(initialPosition);
            yield return new WaitUntil(IsNPCFinishedMoving);

            // Esperar el tiempo de inactividad
            Debug.Log($"Estado: Esperando {idleTimeAtSpawn}s antes de iniciar el siguiente ciclo.");
            yield return new WaitForSeconds(idleTimeAtSpawn);

            Debug.Log("--- CICLO COMPLETADO. REINICIANDO... ---");
            // El bucle while(true) reinicia, y el Paso 1 lo moverá de nuevo al mostrador.
        }
    }
}