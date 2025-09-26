using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MMBotonIniciarPartida : MonoBehaviour
{
    public Button iniciarPartidaButton; // Botón para iniciar la partida

    void Start()
    {
        // Verificar si el botón está asignado en el Inspector
        if (iniciarPartidaButton != null)
        {
            iniciarPartidaButton.onClick.AddListener(() => StartCoroutine(IniciarPartida()));
        }
        else
        {
            Debug.LogError("Botón para iniciar partida no está asignado en el Inspector.");
        }
    }

    private IEnumerator IniciarPartida()
    {
        Debug.Log("Botón 'Iniciar Partida' presionado, cargando escena 'Nivel 1'.");
        yield return null; // No hay sonido, así que no es necesario esperar
        SceneManager.LoadScene("Nivel 1");
    }
}