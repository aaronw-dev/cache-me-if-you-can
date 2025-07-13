using NaughtyAttributes;
using UnityEngine;

public abstract class IInteractable : MonoBehaviour
{
    [Header("Interaction")]
    [ReadOnly]
    public bool hovering;
    [ReadOnly]
    public bool grabbing;


    public abstract void HoverEnter();
    public abstract void HoverExit();
    public abstract void Grab();
    public abstract void Drop();
    public abstract void DropOn(IInteractable dropped);
}