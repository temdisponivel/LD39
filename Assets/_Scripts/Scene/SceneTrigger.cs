using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public string SceneToGoTo;
    public string TagToCollideWith;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.root.gameObject.CompareTag(TagToCollideWith))
        {
            SceneHandler.Instance.ChangeScene(SceneToGoTo);
        }
    }
}
