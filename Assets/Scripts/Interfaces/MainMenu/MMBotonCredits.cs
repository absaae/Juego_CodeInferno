using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MMBotonCredits : MonoBehaviour
{
    public Button creditsButton;

    void Start()
    {
        
        if (creditsButton != null)
        {
            creditsButton.onClick.AddListener(() => StartCoroutine(Credits()));
        }
        else
        {
            Debug.LogError("Botón para iniciar los creditos no está asignado en el Inspector.");
        }
    }

    private IEnumerator Credits()
    {
        Debug.Log("Botón 'Creditos' presionado, cargando escena 'Credits'.");
        yield return null; 
        SceneManager.LoadScene("Credits");
    }
}