using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WLScreen : MonoBehaviour
{
    public Button ReturnMM; 

    void Start()
    {
        if (ReturnMM != null)
        {
            ReturnMM.onClick.AddListener(() => StartCoroutine(MainMenu()));
        }
        else
        {
            Debug.LogError("Botón para regresar al menú no está asignado en el Inspector.");
        }
    }

    private IEnumerator MainMenu()
    {
        Debug.Log("Botón 'Menú principal' presionado, cargando escena 'MainMenu'.");


        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");


        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Escena 'MainMenu' cargada.");
    }
}
