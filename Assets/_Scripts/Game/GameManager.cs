using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController PlayerPrefab;
    public SceneHandler SceneHandler;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        CameraController.Instance.Active = false;

        // Force the gade to happen
        var sprite = SceneHandler.FadeSprite;
        if (sprite.color.a < 1)
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);

        SceneHandler.FadeOut();

        Instantiate(PlayerPrefab.gameObject);
        SceneConfig.Instance.Setup();

        yield return SceneHandler.Instance.FadeOut();

        PlayerController.Instance.Active = true;
        CameraController.Instance.Active = true;
    }
}