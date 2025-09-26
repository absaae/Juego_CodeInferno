using UnityEngine;
using UnityEngine.UI; 

public class HealthUI : MonoBehaviour
{
    public PlayerStats playerStats; // Referencia al script PlayerStats
    public Text healthText; // Referencia al componente Text (o TextMeshProUGUI)

    void Update()
{
    // Actualizar el texto de salud con la vida actual del jugador
    if (playerStats != null && healthText != null)
    {
        // Asegurarse de que la vida no se muestre como un número negativo
        int healthToDisplay = Mathf.Max(0, Mathf.RoundToInt(playerStats.currentHealth));
        healthText.text = "HP: " + healthToDisplay.ToString();
    }
}
}
