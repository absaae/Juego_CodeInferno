using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; 

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        // Verificar si se presiona la tecla Esc para pausar/continuar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true); 
        Time.timeScale = 0; // Detiene el tiempo
    }

    public void Continue()
    {
        pausePanel.SetActive(false); 
        Time.timeScale = 1; // Restaura el tiempo
    }

    public void MainMenu() 
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("MainMenu"); 
    }
}
