using UnityEngine;

public class CacheDropper : IInteractable
{
    public ParticleSystem dropEffects;
    public MemoryLevel memoryLevel;
    public override void DropOn(IInteractable dropped)
    {
        dropEffects.Play();
        Destroy(dropped.gameObject);
    }

    public override void Drop()
    {
    }

    public override void Grab()
    {
    }

    public override void HoverEnter()
    {
    }

    public override void HoverExit()
    {
    }
}
