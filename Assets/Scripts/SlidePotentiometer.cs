using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SlidePotentiometer : IInteractable
{
    [ReadOnly]
    public Vector3 grabStartPosition;
    public Vector3 handleStartPosition;
    public Transform handle;
    [MinMaxSlider(-5, 5)]
    public Vector2 sliderLimits = new Vector2(-2.8f, 2.8f);
    public UnityEvent<float> onValueChanged;
    public override void Grab()
    {
        handle.GetComponent<Collider>().enabled = false;
        handleStartPosition = handle.localPosition;
        grabStartPosition = WorldInteractionManager.global.mouseWorldPosition;
    }
    public override void Drop()
    {
        handle.GetComponent<Collider>().enabled = true;
    }
    public float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }

    void Update()
    {
        if (grabbing)
        {
            Vector3 grabOffset = WorldInteractionManager.global.mouseWorldPosition - grabStartPosition;
            float newX = Mathf.Clamp(handleStartPosition.x + grabOffset.x, sliderLimits.x, sliderLimits.y);
            handle.localPosition = new Vector3(newX, handle.localPosition.y, handle.localPosition.z);
            float mappedValue = Map(handle.localPosition.x, sliderLimits.x, sliderLimits.y, 0, 1);
            Debug.Log("Slide potentiometer: " + mappedValue.ToString());
            onValueChanged.Invoke(mappedValue);
        }
    }
    public void SetSlider(float value)
    {
        float mappedValue = Map(value, 0, 1, sliderLimits.x, sliderLimits.y);
        handle.localPosition = new Vector3(mappedValue, handle.localPosition.y, handle.localPosition.z);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position + Vector3.right * (sliderLimits.x - 0.05f), new Vector3(0.1f, 1, 1));
        Gizmos.DrawCube(transform.position + Vector3.right * (sliderLimits.y + 0.05f), new Vector3(0.1f, 1, 1));
    }
    public override void HoverEnter() { }
    public override void HoverExit() { }
    public override bool DropOn(IInteractable dropped) { return false; }
}
