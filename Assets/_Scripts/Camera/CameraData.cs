using System;
using UnityEngine;

[Serializable]
public class CameraData
{
    public float FollowTransformVelocity;
    public RigidbodyConstraints2D Constraints;
    public Transform TransformToFollow;
    public Vector2 Offset;
    public Vector2 DeadZone;
    public bool ManualUpdate;
}