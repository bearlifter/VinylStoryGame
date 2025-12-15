using UnityEngine;

public class GameFlow : MonoBehaviour
{
    // 1. Singleton para acceso global
    public static GameFlow Instance;

    // 2. Variable de Control CLAVE
    // << isShelfClickable se encuentra aquí >>
    [Header("Control de Interactividad")]
    [Tooltip("Si es TRUE, los objetos del mundo responden al clic. Si es FALSE, no.")]
    public bool isShelfClickable = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para cambiar el estado desde cualquier otro script (opcional)
    public void SetShelfClickable(bool state)
    {
        isShelfClickable = state;
        Debug.Log($"Interactividad (isShelfClickable) establecida a: {state}");
    }
}