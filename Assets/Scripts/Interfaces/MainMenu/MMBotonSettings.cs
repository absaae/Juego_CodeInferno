using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MMBotonSettings : MonoBehaviour
{
    public Button settingsButton; 

    void Start()
    {
 
        if (settingsButton != null)
        {
            settingsButton.onClick.AddListener(() => StartCoroutine(Settings()));
        }
        else
        {
            Debug.LogError("Botón para ir a configuración no está asignado en el Inspector.");
        }
    }

    private IEnumerator Settings()
    {
        Debug.Log("Botón 'Settings' presionado, cargando escena 'Controles'.");
        yield return null; 
        SceneManager.LoadScene("Settings");
    }
}