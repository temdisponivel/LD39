using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;
    public SpriteRenderer FadeSprite;
    public float FadeDuration;

    void Awake()
    {
        Instance = this;
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(InnerChangeScene(sceneName));
    }

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(0));
    }

    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(1));
    }

    private IEnumerator InnerChangeScene(string sceneName)
    {
        PlayerController.Instance.Active = false;
        yield return StartCoroutine(Fade(1));
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(Fade(0));

        SceneConfig.Instance.Setup();

        PlayerController.Instance.Active = true;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        var fadeColor = FadeSprite.color;
        var originalColor = FadeSprite.color;
        fadeColor.a = targetAlpha;
        var delta = 0f;
        while (FadeSprite.color != fadeColor)
        {
            FadeSprite.color = Color.Lerp(originalColor, fadeColor, delta);
            delta += ((1 / FadeDuration) * Time.deltaTime);
            yield return null;
        }
    }
}