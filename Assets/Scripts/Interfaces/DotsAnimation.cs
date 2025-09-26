using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotsAnimation : MonoBehaviour
{
    public Text textComponent;
    public float delay = 0.5f;

    private string baseText = "";
    private int dotCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        baseText = textComponent.text;
        StartCoroutine(AnimateDots());
        
    }

    IEnumerator AnimateDots()
    {
        while(true)
        {
            dotCount = (dotCount + 1) % 4;
            textComponent.text = baseText + new string('.', dotCount);
            yield return new WaitForSeconds(delay);
        }
    }

}
