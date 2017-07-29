using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    public Light LanternLight;
    public LanternData Data;

    public float DefaultLightIntensity;

    private float _currentPower;
    private float _currentIntensity;

    private void Update()
    {
        if (_currentPower <= 0)
            _currentPower += Data.PowerRegenPerSecond * Time.deltaTime;
        else
        {
            if (Input.GetButton(Data.BurstButton))
            {
                _currentIntensity = Data.NormalIntensity * Data.BurstIntensityMultiplier;
                _currentPower -= Data.BurstDrainPowerPerSecond * Time.deltaTime;

                if (_currentPower <= 0)
                {
                    _currentPower = 0;
                    _currentIntensity = Data.NormalIntensity;
                }
            }
        }

        LanternLight.intensity = DefaultLightIntensity + _currentIntensity;
    }
}
