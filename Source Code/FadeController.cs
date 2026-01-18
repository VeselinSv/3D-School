using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadePanel;        
    public float fadeDuration = 1f; 
    public string sceneToLoad;      

    private void Awake()
    {
        if (fadePanel != null)
        {
            Color c = fadePanel.color;
            c.a = 0f;
            fadePanel.color = c;
        }
    }

    public void FadeOutAndLoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
            StartCoroutine(FadeAndLoadAsync(sceneToLoad));
    }

    private IEnumerator FadeAndLoadAsync(string sceneName)
    {
        Color c = fadePanel.color;
        c.a = 0f;
        fadePanel.color = c;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadePanel.color = c;
            yield return null; 
        }
        c.a = 1f;
        fadePanel.color = c;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; 

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
                asyncLoad.allowSceneActivation = true;

            yield return null;
        }
    }
}
