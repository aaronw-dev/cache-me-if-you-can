using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SlideSwitch : IInteractable
{
    [OnValueChanged("UpdateSwitchPosition")]
    public bool switchEnabled;
    public Transform handle;
    [MinMaxSlider(-5, 5)]
    public Vector2 switchPositions = new Vector2(-0.4f, 0);
    public float lerpSpeed = 5;
    public UnityEvent<bool> onValueChanged;
    public override void Grab()
    {
        switchEnabled = !switchEnabled;
        onValueChanged.Invoke(switchEnabled);
    }
    void Update()
    {
        handle.localPosition = Vector3.Lerp(handle.localPosition, new Vector3(switchEnabled ? switchPositions.x : switchPositions.y, 0), Time.deltaTime * lerpSpeed);
    }
    void UpdateSwitchPosition()
    {
        handle.localPosition = new Vector3(switchEnabled ? switchPositions.x : switchPositions.y, 0);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position + Vector3.right * (switchPositions.x - 0.05f), new Vector3(0.1f, 1, 1));
        Gizmos.DrawCube(transform.position + Vector3.right * (switchPositions.y + 0.05f), new Vector3(0.1f, 1, 1));
    }
    public override void Drop() { }
    public override void HoverEnter() { }
    public override void HoverExit() { }
    public override bool DropOn(IInteractable dropped) { return false; }
}
