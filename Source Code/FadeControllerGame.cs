using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInGame : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadePanel;        
    public float fadeDuration = 1f; 

    private void Awake()
    {
        if (fadePanel != null)
        {
            Color c = fadePanel.color;
            c.a = 1f;
            fadePanel.color = c;
        }
    }

    private void Start()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        Color c = fadePanel.color;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadePanel.color = c;
            yield return null;
        }

        c.a = 0f;
        fadePanel.color = c;
    }
}
