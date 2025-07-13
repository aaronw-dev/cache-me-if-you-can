using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum MemoryLevel
{
    L1,
    L2,
    L3,
}
public class MemoryRequest : IInteractable
{
    [Header("Interactions")]
    public Vector3 grabOffset = new Vector3(0, 2, 0);
    [HideInInspector]
    public Vector3 positionTarget, angleTarget, lastWorldPosition;
    public Vector3 grabAngles = new Vector3(0, 30, 0);
    public float moveLerpSpeed = 5;

    [Header("Visuals")]
    public GameObject viewModel;
    public TMP_Text memoryLevelText;
    public TMP_Text memoryAddressText;
    public MemoryLevel memoryLevel;
    public float expiryTimer = 5;
    private float startTimer;
    public Image expiryBar;
    public ParticleSystem destroyParticles;
    [Header("Game")]
    public float temperatureImpact = 5;
    public int score = 200;
    private void Start()
    {
        positionTarget = transform.position;
        startTimer = expiryTimer;
    }
    void Update()
    {
        if (grabbing)
        {
            lastWorldPosition = WorldInteractionManager.global.mouseWorldPosition;
            positionTarget = lastWorldPosition + grabOffset;
            angleTarget = grabAngles;
            transform.position = Vector3.Lerp(transform.position, positionTarget, Time.deltaTime * moveLerpSpeed);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(angleTarget), Time.deltaTime * moveLerpSpeed);

        expiryTimer -= Time.deltaTime;
        expiryBar.fillAmount = expiryTimer / startTimer;

        if (expiryTimer <= 0)
        {
            destroyParticles.transform.parent = null;
            destroyParticles.Play();
            MemoryRequestSpawner.global.spawnMemoryRequest();
            SoundManager.global.Error();
            Destroy(gameObject);
        }
    }

    public override void HoverEnter()
    {
    }

    public override void HoverExit()
    {
    }

    public override void Grab()
    {
        GetComponentInChildren<Collider>().enabled = false;
    }

    public override void Drop()
    {
        GetComponentInChildren<Collider>().enabled = true;
        angleTarget = Vector3.zero;
    }

    public override void DropOn(IInteractable dropped)
    {
    }
}
