using UnityEngine;

public class Cable : MonoBehaviour
{
    public bool IsActive;

    public Renderer[] Renderers;
    public Material LitMaterial;
    public Material UnlitMaterial;

    private void Reset()
    {
        Renderers = GetComponentsInChildren<Renderer>();
    }

    private void Awake()
    {
        if (IsActive)
            Lit();
        else
            Unlit();
    }

    public void Lit()
    {
        IsActive = true;
        for (int i = 0; i < Renderers.Length; i++)
            Renderers[i].sharedMaterial = LitMaterial;
    }

    public void Unlit()
    {
        IsActive = false;
        for (int i = 0; i < Renderers.Length; i++)
            Renderers[i].sharedMaterial = UnlitMaterial;
    }
}