using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorPosition = 0;
    public Material conveyorMaterial;

    void Update()
    {
        conveyorMaterial.mainTextureOffset = conveyorPosition * Vector2.up;
    }
}
