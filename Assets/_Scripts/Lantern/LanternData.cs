using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LanternData
{
    public float NormalIntensity;
    public float Power;

    public float BurstIntensityMultiplier;

    public string BurstButton;
    public float BurstDrainPowerPerSecond;

    public float PowerRegenPerSecond;
}
