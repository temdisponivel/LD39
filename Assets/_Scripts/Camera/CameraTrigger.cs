using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraData DataToSetOnCamera;
    public string TagToCollidWith;

    public bool ResetOnTriggerExit;

    private CameraData _cameraDataCache;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.root.gameObject.CompareTag(TagToCollidWith))
        {
            _cameraDataCache = CameraController.Instance.Data;
            CameraController.Instance.SetData(DataToSetOnCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.root.gameObject.CompareTag(TagToCollidWith))
            CameraController.Instance.SetData(_cameraDataCache);
    }
}