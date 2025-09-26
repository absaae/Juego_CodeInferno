using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextLevelAfterDelay());
    }

    IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(3f); 
        SceneManager.LoadScene("Nivel 2"); 
    }
}
