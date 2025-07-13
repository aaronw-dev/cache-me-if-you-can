using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorPosition = 0;
    public Material conveyorMaterial;

    public bool isMainMenuBelt;
    public Transform beltItems;
    void Update()
    {
        conveyorMaterial.mainTextureOffset = conveyorPosition * Vector2.up;
        if (isMainMenuBelt)
        {
            conveyorPosition = -beltItems.localPosition.x;
        }
    }
}
