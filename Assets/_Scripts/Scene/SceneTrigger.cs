using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public string SceneToGoTo;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.root.gameObject.CompareTag("Player"))
        {
            SceneHandler.Instance.ChangeScene(SceneToGoTo);
        }
    }
}
