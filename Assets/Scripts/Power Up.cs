using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum PowerUpType
{
    DEFRAG,
    OVERCLOCK,
    THERMALDUMP,
}
public class PowerUp : IInteractable
{
    [Header("Interactions")]
    public Vector3 grabOffset = new Vector3(0, 2, 0);
    [HideInInspector]
    public Vector3 positionTarget, angleTarget, lastWorldPosition;
    public Vector3 grabAngles = new Vector3(0, 30, 0);
    public float moveLerpSpeed = 5;

    [Header("Visuals")]
    public GameObject viewModel;
    public float expiryTimer = 5;
    private float startTimer;
    public Image expiryBar;
    public ParticleSystem useParticles;
    public ParticleSystem destroyParticles;
    [Header("Game")]
    public int score = 200;
    public float overclockDuration;
    public PowerUpType powerUpType;
    public AudioClip powerUpSound;
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

        if (StatsManager.global.gameRunning)
            expiryTimer -= Time.deltaTime;
        expiryBar.fillAmount = expiryTimer / startTimer;

        if (expiryTimer <= 0)
        {
            destroyParticles.transform.parent = null;
            destroyParticles.Play();
            MemoryRequestSpawner.global.spawnMemoryRequest();
            SoundManager.global.Error();
            StatsManager.global.score += score;
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
        useParticles.Play();
        useParticles.transform.parent = null;
        SoundManager.global.PlaySound(powerUpSound, 1);
        switch (powerUpType)
        {
            case PowerUpType.DEFRAG:
                {
                    MemoryRequestSpawner.global.DefragQueue();
                    break;
                }
            case PowerUpType.OVERCLOCK:
                {
                    StatsManager.global.Overclock(overclockDuration);
                    break;
                }
            case PowerUpType.THERMALDUMP:
                {
                    CPUTemperature.global.temperature -= 30;
                    break;
                }
        }
        MemoryRequestSpawner.global.spawnMemoryRequest();
        Destroy(gameObject);
    }

    public override void Drop()
    {
    }

    public override bool DropOn(IInteractable dropped)
    {
        return false;
    }
}
