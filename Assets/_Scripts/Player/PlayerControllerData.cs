﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class PlayerControllerData
{
    public float HorizontalForce;
    public float MaxHorizontalVelocity;
    public ForceMode2D HorizontalForceMode;
    public float OnAirHorizontalForceMultiplier;

    public float MinJumpForce;
    public float MaxJumpForce;
    public float MaxTimeHoldingJumpButton;
    public ForceMode2D JumpForceMode;

    public string JumpButtonName;
    public string HorizontalAxisName;

    [Tooltip("The ammount to which displace the camera to look in 'front' of the player")]
    public float CameraDisplacementWhenMoving;

    void Reset()
    {
        JumpButtonName = "Jump";
        HorizontalAxisName = "Horizontal";
    }
}
