using UnityEngine;
using UnityEngine.UI;

public class CacheDropper : IInteractable
{
    public ParticleSystem dropEffects;
    public AnimationCurve cooldownCurve = new AnimationCurve();
    private float cooldownTimer;
    public MemoryLevel memoryLevel;
    public Image cooldownImage;
    private void Start()
    {
        cooldownTimer = 0;
    }
    private float cooldownDuration()
    {
        return cooldownCurve.Evaluate(StatsManager.global.gameDuration);
    }
    private void Update()
    {
        if (!StatsManager.global.overclocked)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownTimer = Mathf.Clamp(cooldownTimer, 0, cooldownDuration());
        }
        else
        {
            cooldownTimer = 0;
        }
        cooldownImage.fillAmount = cooldownTimer / cooldownDuration();
    }
    public override bool DropOn(IInteractable dropped)
    {
        if (cooldownTimer <= 0 || StatsManager.global.overclocked)
        {
            dropEffects.Play();
            Destroy(dropped.gameObject);
            cooldownTimer = cooldownDuration();
            return true;
        }
        return false;
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
