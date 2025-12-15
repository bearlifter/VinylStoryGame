using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // 1. Referencia al NPCFlow para comunicarse
    private NPCFlow npcFlow;

    void Start()
    {
        // Buscamos el objeto NPC (asumiendo que tiene la etiqueta "NPC")
        GameObject npcObject = GameObject.FindWithTag("NPC");

        if (npcObject != null)
        {
            npcFlow = npcObject.GetComponent<NPCFlow>();
        }

        if (npcFlow == null)
        {
            // Esto es una advertencia útil si olvidas asignar la etiqueta o el script.
            Debug.LogError("ClickManager: NPCFlow no encontrado. Asegúrate de que el objeto NPC tenga la etiqueta 'NPC' y el script NPCFlow.");
        }
    }

    void Update()
    {
        // Bloqueo global de interactividad
        if (!GameFlow.Instance.isShelfClickable)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Usamos RaycastAll para detectar todos los colisionadores debajo del ratón
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            // Iteramos sobre todos los resultados
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    // Buscamos el componente CoordinateData
                    CoordinateData clickedData = hit.collider.GetComponent<CoordinateData>();

                    if (clickedData != null)
                    {
                        // Si encontramos los datos, gestionamos el clic e INICIAMOS el flujo del NPC
                        HandlePointClick(clickedData);

                        // Detenemos la iteración/el Update, ya que ya hemos procesado el clic
                        return;
                    }
                }
            }
        }
    }

    private void HandlePointClick(CoordinateData data)
    {
        Vector3 coordinates = data.targetCoordinates;
        string name = data.locationName;

        Debug.Log($"Clic exitoso en: {name}. Coordenadas: {coordinates}");

        // 2. **Llamada CLAVE al NPCFlow**
        // Pasamos las coordenadas al NPCFlow. Esto despierta el bucle de la corrutina 
        // que estaba esperando la variable 'hasClickedShelf' se volviera TRUE.
        if (npcFlow != null)
        {
            npcFlow.SetTargetAndSignalClick(coordinates);
        }
        else
        {
            Debug.LogError("NPCFlow no está asignado. No se puede mover el NPC.");
        }
    }
}