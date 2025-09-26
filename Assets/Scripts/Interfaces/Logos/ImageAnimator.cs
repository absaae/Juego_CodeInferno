using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI
using UnityEngine.SceneManagement; // Necesario para cargar escenas
using System.Collections;

public class ImageAnimator : MonoBehaviour
{
    public Image imageToFade; // Asigna la imagen en el Inspector
    public float delayBeforeShowing = 2f; // Tiempo antes de mostrar la imagen
    public float displayTime = 1f; // Tiempo que la imagen estará visible
    public float fadeDuration = 1f; // Tiempo de desvanecimiento

    private void Start()
    {
        // Comienza la corutina
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Espera antes de mostrar la imagen
        yield return new WaitForSeconds(delayBeforeShowing);
        
        // Muestra la imagen
        imageToFade.gameObject.SetActive(true);
        Color imageColor = imageToFade.color;
        imageColor.a = 1; // Establece la opacidad a 1 (visible)
        imageToFade.color = imageColor;

        // Espera el tiempo de visualización
        yield return new WaitForSeconds(displayTime);

        // Comienza a desvanecer la imagen
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Calcula el valor alfa
            imageColor.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            imageToFade.color = imageColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegúrate de que la imagen esté completamente transparente
        imageColor.a = 0;
        imageToFade.color = imageColor;

        // Carga la escena MainMenu
        SceneManager.LoadScene("MainMenu");
    }
}
