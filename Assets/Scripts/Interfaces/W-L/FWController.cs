using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FWController : MonoBehaviour
{
    public TextMeshProUGUI enemiesRemainingText;
    public TextMeshProUGUI levelInfoText;
    public GameObject restartButton;

    void Start()
    {
        // Mostrar la cantidad de enemigos restantes
        enemiesRemainingText.text = "Enemigos restantes: " + EnemySpawner.enemiesRemaining.ToString();

        // Comprobar si llegamos desde el nivel 1 o nivel 2
        if (EnemySpawner.levelFlag == 1)
        {
            levelInfoText.text = "PERDISTE";
        }
        else if (EnemySpawner.levelFlag == 2 && !EnemySpawner.levelCompleted)
        {
            levelInfoText.text = "PERDISTE";
        }
        else if (EnemySpawner.levelFlag == 2 && EnemySpawner.levelCompleted)
        {
            levelInfoText.text = "¡GANASTE!";
            StartCoroutine(WaitAndLoadNextLevel());
        }

        restartButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnRestartButtonClicked);
    }

    IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(3f); // Esperar 3 segundos
        SceneManager.LoadScene("Nivel 2"); // Cargar la escena 'Nivel 2'
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;

        if (EnemySpawner.levelFlag == 1)
        {
            SceneManager.LoadScene("Nivel 1"); // Volver al nivel 1 si la bandera es 1
        }
        else if (EnemySpawner.levelFlag == 2)
        {
            SceneManager.LoadScene("Nivel 2"); // Cargar la escena 'Win' si la bandera es 2
        }
    }
}
