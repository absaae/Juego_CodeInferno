using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MMBotonControles : MonoBehaviour
{
    public Button controlesButton;

    void Start()
    {
        
        if (controlesButton != null)
        {
            controlesButton.onClick.AddListener(() => StartCoroutine(Controles()));
        }
        else
        {
            Debug.LogError("Botón para iniciar los controles no está asignado en el Inspector.");
        }
    }

    private IEnumerator Controles()
    {
        Debug.Log("Botón 'Controles' presionado, cargando escena 'Controles'.");
        yield return null; 
        SceneManager.LoadScene("Controles");
    }
}