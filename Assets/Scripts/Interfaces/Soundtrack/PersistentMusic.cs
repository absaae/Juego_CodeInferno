using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;
    private AudioSource audioSource;

    private const string VolumeKey = "musicVolume";  // Clave para almacenar el volumen en PlayerPrefs

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  
            audioSource = GetComponent<AudioSource>();  

            // Cargar el volumen guardado al iniciar
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1.0f);
            audioSource.volume = savedVolume;  // Ajustar el volumen inicial
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Fail")  
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1.0f); 
            audioSource.volume = savedVolume * 0.3f;  // Reducir el volumen basado en el valor guardado
        }
        else if (scene.name == "MainMenu")  
        {
            audioSource.Stop();
            audioSource.Play();
            audioSource.volume = 1.0f;  
        }
        else
        {
            audioSource.volume = PlayerPrefs.GetFloat(VolumeKey, 1.0f);  // Ajustar a valor guardado en otras escenas
        }
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }
}
