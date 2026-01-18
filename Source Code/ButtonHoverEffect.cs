using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Scale")]
    public float hoverScale = 1.15f;
    public float animationSpeed = 5f;

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color hoverColor = Color.cyan;
    public Color pressedColor = Color.gray;

    private RectTransform rectTransform;
    private UnityEngine.UI.Image image;
    private Vector3 originalScale;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<UnityEngine.UI.Image>();

        originalScale = rectTransform.localScale;
        targetScale = originalScale;   
        image.color = normalColor;
    }

    void Update()
    {
        rectTransform.localScale = Vector3.Lerp(
            rectTransform.localScale,
            targetScale,
            Time.deltaTime * animationSpeed
        );
    }

    private Vector3 targetScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        image.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.color = hoverColor;
    }
}
