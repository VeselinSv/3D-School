using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    public Image fill;           
    public Image staminaImage;   
    public TextMeshProUGUI labelText;

    public void SetStamina(float current, float max)
    {
        float pct = current / max;

        if (fill)
            fill.fillAmount = pct;

        if (staminaImage)
        {
            staminaImage.gameObject.SetActive(current > 0);
        }

        if (labelText)
            labelText.text = $"{Mathf.RoundToInt(current)}/{max}";
    }
}
