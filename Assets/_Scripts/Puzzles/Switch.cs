public class Switch : EnergyDevice
{
    public bool IsActivated;
    public Cable[] OutCables;

    public override void Interact()
    {
        if (IsActivated)
        {
            for (int i = 0; i < OutCables.Length; i++)
                OutCables[i].Unlit();

            IsActivated = false;
        }
        else
        {
            if (IsEnabled)
            {
                for (int i = 0; i < OutCables.Length; i++)
                    OutCables[i].Lit();

                IsActivated = true;
            }
        }
    }
}