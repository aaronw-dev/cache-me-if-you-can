using UnityEngine;
using NaughtyAttributes;
public class WorldInteractionManager : MonoBehaviour
{
    [Header("Interaction")]
    [ReadOnly]
    public Vector3 mouseWorldPosition;
    [ReadOnly]
    public IInteractable currentlyHoveringInteractable;
    [ReadOnly]
    public IInteractable lastHoveredInteractable;
    [ReadOnly]
    public IInteractable currentlyHeldInteractable;

    private Camera cam;
    public static WorldInteractionManager global;

    [Header("SFX")]
    public AudioClip success;
    void Start()
    {
        cam = GetComponent<Camera>();
        global = this;
    }
    void Update()
    {
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.red);
        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo))
        {
            mouseWorldPosition = hitInfo.point;
            currentlyHoveringInteractable = hitInfo.collider.GetComponentInParent<IInteractable>();
            if (currentlyHoveringInteractable)
            {
                if (lastHoveredInteractable != currentlyHoveringInteractable && lastHoveredInteractable != null)
                {
                    lastHoveredInteractable.HoverExit();
                    lastHoveredInteractable.hovering = false;
                }
                currentlyHoveringInteractable.HoverEnter();
                currentlyHoveringInteractable.hovering = true;

                if (Input.GetMouseButtonDown(0))
                {
                    currentlyHoveringInteractable.Grab();
                    currentlyHoveringInteractable.grabbing = true;
                    currentlyHeldInteractable = currentlyHoveringInteractable;
                }
            }
            else
            {
                if (lastHoveredInteractable != null)
                {
                    lastHoveredInteractable.HoverExit();
                    lastHoveredInteractable.hovering = false;
                }
            }
            if (Input.GetMouseButtonUp(0) && currentlyHeldInteractable != null)
            {
                if (currentlyHoveringInteractable != null && currentlyHoveringInteractable.GetType() == typeof(CacheDropper))
                {
                    MemoryRequest request = currentlyHeldInteractable.GetComponent<MemoryRequest>();
                    CacheDropper cacheDropper = currentlyHoveringInteractable.GetComponent<CacheDropper>();
                    if (request.memoryLevel == cacheDropper.memoryLevel)
                    {
                        currentlyHoveringInteractable.DropOn(currentlyHeldInteractable);
                        MemoryRequestSpawner.global.spawnMemoryRequest();
                        CPUTemperature.global.temperature -= request.temperatureImpact;

                        StatsManager.global.successfulOperations++;
                        StatsManager.global.streak++;
                        StatsManager.global.score += request.score;

                        SoundManager.global.PlaySound(success, 0.5f);
                    }
                }

                currentlyHeldInteractable.grabbing = false;
                currentlyHeldInteractable.Drop();
                currentlyHeldInteractable = null;
            }
            if (currentlyHoveringInteractable != null && !currentlyHeldInteractable)
            {
                lastHoveredInteractable = currentlyHoveringInteractable;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mouseWorldPosition, 0.1f);
    }
}
