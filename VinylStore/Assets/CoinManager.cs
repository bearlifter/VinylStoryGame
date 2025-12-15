using UnityEngine;
using TMPro; // Asegúrate de incluir esta librería para TextMeshPro

public class CoinManager : MonoBehaviour
{
    // 1. Patrón Singleton
    public static CoinManager Instance;

    // 2. Variables
    private int _currentCoins = 0;
    public TextMeshProUGUI coinTextUI; // Asigna esto en el Inspector con el objeto CoinText

    // 3. Propiedad pública para acceder a las monedas de forma segura
    public int CurrentCoins
    {
        get { return _currentCoins; }
    }

    // Método Awake se llama al iniciar, antes que Start
    void Awake()
    {
        // Implementación del Singleton
        if (Instance == null)
        {
            Instance = this;
            // Opcional: Para que persista entre escenas (si es un juego complejo)
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Inicializa el texto de la UI con la cantidad inicial (0)
        UpdateCoinUI();
    }

    // 4. Método para añadir monedas
    public void AddCoins(int amount)
    {
        if (amount < 0) return; // Evita valores negativos accidentales
        _currentCoins += amount;
        UpdateCoinUI();
        Debug.Log("Monedas añadidas. Total: " + _currentCoins);
    }

    // 5. Método para gastar/quitar monedas
    public bool SpendCoins(int amount)
    {
        if (amount < 0) amount = -amount; // Asegura que sea un valor positivo

        if (_currentCoins >= amount)
        {
            _currentCoins -= amount;
            UpdateCoinUI();
            Debug.Log("Monedas gastadas. Total: " + _currentCoins);
            return true; // Éxito en el gasto
        }
        else
        {
            Debug.Log("No hay suficientes monedas para gastar: " + amount);
            return false; // Fracaso en el gasto
        }
    }

    // 6. Método para actualizar la UI
    private void UpdateCoinUI()
    {
        if (coinTextUI != null)
        {
            // Formatea el texto para que siempre tenga un mínimo de dígitos (ej: 005)
            coinTextUI.text = _currentCoins.ToString("D3");
        }
    }
}