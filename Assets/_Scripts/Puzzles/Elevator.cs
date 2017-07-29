using UnityEngine;

// This is very similar to the Swith component, except this one does not have out cables
public class Elevator : EnergyDevice
{
    public string SceneToGoTo;

    public override void Interact()
    {
        if (IsEnabled)
            SceneHandler.Instance.ChangeScene(SceneToGoTo);
    }
}