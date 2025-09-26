using UnityEngine;
using UnityEngine.UI;

public class MMBotonQuit: MonoBehaviour
{
    public Button quitButton;

    void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("Botón para salir del juego no está asignado en el Inspector.");
        }
    }

    private void QuitGame()
    {
        Debug.Log("Botón 'Controles' presionado, cerrando el juego.");
        Application.Quit();  

    
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #endif
    }
}
