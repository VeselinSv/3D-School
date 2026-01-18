using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TMPTextHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text tmpText; 
    public Color hoverColor = Color.red; 
    private Color originalColor; 

    void Start()
    {
        if (tmpText == null)
            tmpText = GetComponent<TMP_Text>();

        originalColor = tmpText.color; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmpText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmpText.color = originalColor;
    }
}
