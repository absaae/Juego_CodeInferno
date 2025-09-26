using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;
    public Color highlightColor = Color.yellow;
    private Color originalColor;

    void Start()
    {
        originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = originalColor;
    }
}
