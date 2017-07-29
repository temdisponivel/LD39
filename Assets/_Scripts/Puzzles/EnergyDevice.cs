using UnityEngine;

public class EnergyDevice : MonoBehaviour
{
    public bool IsEnabled;

    public Cable[] InCables;
    
    public Material LitMaterial;
    public Material UnlitMaterial;
    public Renderer[] Renderers;

    void Awake()
    {
        if (IsEnabled)
            Lit();
        else
            Unlit();
    }

    // I know this is ugly, but it's very handy
    public virtual void Interact() { }

    // I know there's no need to do this on update
    // we could just register on some event and validate when
    // the event is fired, but I feel like that would be overkill
    private void Update()
    {
        ValidateIfEnabled();
    }
    
    public void ValidateIfEnabled()
    {
        var enabled = false;
        for (int i = 0; i < InCables.Length; i++)
            enabled |= InCables[i].IsActive;

        // just got enabled
        if (!IsEnabled && enabled)
            Lit();
        else if (IsEnabled && !enabled)
            Unlit();
    }

    public void Lit()
    {
        IsEnabled = true;
        for (int i = 0; i < Renderers.Length; i++)
            Renderers[i].sharedMaterial = LitMaterial;
    }

    public void Unlit()
    {
        IsEnabled = false;
        for (int i = 0; i < Renderers.Length; i++)
            Renderers[i].sharedMaterial = UnlitMaterial;
    }
}