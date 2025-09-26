using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public Slider volumeSlider;  
    private AudioSource audioSource;  
    private const string VolumeKey = "musicVolume";  

    private void Start()
    {
        
        PersistentMusic music = FindObjectOfType<PersistentMusic>();
        if (music != null)
        {
            audioSource = music.GetComponent<AudioSource>();

            // Cargar el valor de volumen guardado, o usar 1.0f por defecto si no hay valor guardado
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1.0f);
            volumeSlider.value = savedVolume;  // Actualiza el slider
            audioSource.volume = savedVolume;  // Ajusta el volumen


            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        else
        {
            Debug.LogError("No se encontró el objeto PersistentMusic.");
        }
    }

    
    public void SetVolume(float value)
    {
        Debug.Log("Slider cambiado a: " + value);  
        if (audioSource != null)  
        {
            audioSource.volume = value;  // Cambia el volumen del AudioSource
            PlayerPrefs.SetFloat(VolumeKey, value);  // Guarda el valor en PlayerPrefs
            PlayerPrefs.Save();  // Asegura que el valor se guarde
            Debug.Log("Volumen guardado: " + PlayerPrefs.GetFloat(VolumeKey));  // Verifica el valor guardado
        }
        else
        {
            Debug.LogError("AudioSource no está asignado en el script Control.");
        }
    }
}
