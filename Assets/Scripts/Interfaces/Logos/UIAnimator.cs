using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class UIAnimator : MonoBehaviour
{
    public Image image;
    public TMP_Text textMeshPro; 
    public float fadeDuration = 1f;

    private void Start()
    {
        image.gameObject.SetActive(false);
        textMeshPro.gameObject.SetActive(false);
        
        StartCoroutine(ShowUIAfterDelay(2f));
    }

    private IEnumerator ShowUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        image.gameObject.SetActive(true);
        textMeshPro.gameObject.SetActive(true);

        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene("LocoCI");
    }

    private IEnumerator FadeIn()
    {
        Color imageColor = image.color;
        Color textColor = textMeshPro.color;

        imageColor.a = 0;
        textColor.a = 0;

        image.color = imageColor;
        textMeshPro.color = textColor;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            imageColor.a = Mathf.Clamp01(t / fadeDuration);
            textColor.a = Mathf.Clamp01(t / fadeDuration);

            image.color = imageColor;
            textMeshPro.color = textColor;

            yield return null;
        }

        imageColor.a = 1;
        textColor.a = 1;
        image.color = imageColor;
        textMeshPro.color = textColor;
    }

    private IEnumerator FadeOut()
    {
        Color imageColor = image.color;
        Color textColor = textMeshPro.color;

        imageColor.a = 1;
        textColor.a = 1;

        image.color = imageColor;
        textMeshPro.color = textColor;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            imageColor.a = Mathf.Clamp01(1 - (t / fadeDuration));
            textColor.a = Mathf.Clamp01(1 - (t / fadeDuration));

            image.color = imageColor;
            textMeshPro.color = textColor;

            yield return null;
        }

        imageColor.a = 0;
        textColor.a = 0;
        image.color = imageColor;
        textMeshPro.color = textColor;

        image.gameObject.SetActive(false);
        textMeshPro.gameObject.SetActive(false);
    }
}
