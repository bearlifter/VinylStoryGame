using UnityEngine;

public class CoordinateData : MonoBehaviour
{
    // Esta variable guarda la posición fija del objeto en el mundo.
    [Tooltip("La posición exacta (coordenadas) a la que se moverá el personaje.")]
    public Vector3 targetCoordinates;

    // Nombre para fines de depuración
    public string locationName = "Punto Clicable";

    // Al iniciar, el objeto guarda su posición actual.
    void Start()
    {
        // **ESTO GARANTIZA QUE LA POSICIÓN SEA FIJA:**
        // Guarda la posición inicial del Transform del objeto.
        Debug.Log($"Objeto {locationName} inicializado en: {targetCoordinates}");
    }
}