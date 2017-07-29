using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public bool Active;
    public CameraData Data;
    private Vector2 _currentOffset;

    void Awake()
    {
        Instance = this;
    }

    public void SetData(CameraData cameraData)
    {
        Data = cameraData;
    }

    public void SetContrainsts(RigidbodyConstraints2D constraints)
    {
        Data.Constraints = constraints;
    }

    public void PinToTransformWithCurrentOffset(Transform targetTransform)
    {
        var offset = transform.position - targetTransform.position;
        PinToTransform(targetTransform, offset);
    }

    public void PinToTransform(Transform targetTransform, Vector3 offset)
    {
        Data.TransformToFollow = targetTransform;
        Data.Offset = offset;
    }

    private void LateUpdate()
    {
        if (!Active)
            return;


        var targetPos = (Vector2)Data.TransformToFollow.position + _currentOffset;

        var leftThreshold = GetLeftThresholdPos();
        var rightThreshold = GetRightThresholdPos();

        var leftOffset = GetLeftOffsetPos();
        var rightOffset = GetRightOffsetPos();
        var targetTransformPos = Data.TransformToFollow.position;

        if (targetTransformPos.x < leftOffset.x)
        {
            if (targetTransformPos.x < leftThreshold.x)
                _currentOffset = -Data.Offset;
            else if (targetPos.x < transform.position.x)
                return;
        }
        else if (targetTransformPos.x > rightOffset.x)
        {
            if (targetTransformPos.x > rightThreshold.x)
                _currentOffset = Data.Offset;
            else if (targetPos.x > transform.position.x)
                return;
        }

        targetPos = (Vector2)Data.TransformToFollow.position + _currentOffset;

        if ((Data.Constraints & RigidbodyConstraints2D.FreezePositionX) == RigidbodyConstraints2D.FreezePositionX)
            targetPos.x = transform.position.x;

        if ((Data.Constraints & RigidbodyConstraints2D.FreezePositionY) == RigidbodyConstraints2D.FreezePositionY)
            targetPos.y = transform.position.y;

        var targetVec3 = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetVec3, Data.FollowTransformVelocity * Time.deltaTime);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position - Vector3.up * 10, transform.position + Vector3.up * 10);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Data.TransformToFollow.position - Vector3.up * 10, Data.TransformToFollow.position + Vector3.up * 10);

        Gizmos.color = Color.green;
        Vector3 pos;
        pos = GetRightOffsetPos();
        Gizmos.DrawLine(pos - Vector3.up * 10, pos + Vector3.up * 10);

        Gizmos.color = Color.blue;

        pos = GetLeftOffsetPos();
        Gizmos.DrawLine(pos - Vector3.up * 10, pos + Vector3.up * 10);

        Gizmos.color = Color.cyan;

        pos = GetLeftThresholdPos();
        Gizmos.DrawLine(pos - Vector3.up * 10, pos + Vector3.up * 10);

        Gizmos.color = Color.magenta;

        pos = GetRightThresholdPos();
        Gizmos.DrawLine(pos - Vector3.up * 10, pos + Vector3.up * 10);
    }

#endif

    public Vector2 GetRightOffsetPos()
    {
        return transform.position + (Vector3)_currentOffset;
    }

    public Vector2 GetLeftOffsetPos()
    {
        return transform.position - (Vector3)_currentOffset;
    }

    private Vector3 GetRightThresholdPos()
    {
        var pos = transform.position + (Vector3)_currentOffset;
        return pos + (Vector3)Data.DeadZone;
    }

    private Vector3 GetLeftThresholdPos()
    {
        var pos = transform.position - (Vector3)_currentOffset;
        return pos - (Vector3)Data.DeadZone;
    }
}
