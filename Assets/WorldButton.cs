using UnityEngine;
using UnityEngine.Events;

public class WorldButton : IInteractable
{
    public Transform buttonModel;
    public float hoverDepth = 0.05f;
    public float clickDepth = 0.1f;
    public float lerpSpeed = 5;
    public UnityEvent onClick;
    public bool buttonEnabled;
    public Material enabledMaterial;
    public Material disabledMaterial;
    public float colorLerpSpeed = 5;
    private Renderer buttonRenderer;
    private void Start()
    {
        buttonRenderer = buttonModel.GetComponent<Renderer>();
    }
    private void Update()
    {
        if (grabbing)
            buttonModel.localPosition = Vector3.Lerp(buttonModel.localPosition, Vector3.down * clickDepth, Time.deltaTime * lerpSpeed);
        else if (hovering)
            buttonModel.localPosition = Vector3.Lerp(buttonModel.localPosition, Vector3.down * hoverDepth, Time.deltaTime * lerpSpeed);
        else
            buttonModel.localPosition = Vector3.Lerp(buttonModel.localPosition, Vector3.zero, Time.deltaTime * lerpSpeed);
        buttonRenderer.material.Lerp(buttonRenderer.material, buttonEnabled ? enabledMaterial : disabledMaterial, Time.deltaTime * colorLerpSpeed);
    }
    public void SetButtonEnabled(bool enabled)
    {
        buttonEnabled = enabled;
    }
    public override void HoverEnter() { }

    public override void HoverExit() { }

    public override void Grab()
    {
        if (onClick != null && buttonEnabled)
            onClick.Invoke();
    }

    public override void Drop() { }

    public override void DropOn(IInteractable dropped) { }
}
