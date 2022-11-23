using UnityEngine;
using System.Collections;
using RoguelikeCombat;
using DG.Tweening;

public class PlayerTankHealth : TankHealth
{
    public Transform shieldEnergy;
    public Transform shieldInvert;
    public ShellExplosion invShellPrefab;

    public override void TakeDamage(float amount, Tank origin, ShellExplosion shell = null)
    {
        if (shieldEnergy.gameObject.activeSelf)
            return;

        var rrs = RoguelikeRewardSystem.instance;

        if (rrs.HasPerk(RoguelikeIdentifier.HeavyArmor))
            amount *= 0.6f;
        if (rrs.HasPerk(RoguelikeIdentifier.LightArmor))
            amount *= 0.75f;

        if (rrs.HasPerk(RoguelikeIdentifier.EnergyShield))
        {
            CoolDownSystem.instance.TryTrigger(RoguelikeIdentifier.EnergyShield,
          () =>
          {
              //active EnergyShield
              ActiveEnergyShield();
              amount = 0;
          }, 20.0f);
        }

        if (rrs.HasPerk(RoguelikeIdentifier.InvArmor) && shell != null && origin != null)
        {
            CoolDownSystem.instance.TryTrigger(RoguelikeIdentifier.InvArmor,
          () =>
          {
              ActiveInvArmor(origin, shell);
              amount = 0;
          }, 3.0f);
        }

        if (amount <= 0)
            return;

        base.TakeDamage(amount, origin);
    }

    void ActiveEnergyShield()
    {
        shieldEnergy.gameObject.SetActive(true);

        shieldEnergy.DOKill();
        shieldEnergy.localScale = Vector3.one * 1.0f;
        shieldEnergy.DOScale(3, 0.4f).SetEase(Ease.OutBounce);
        StartCoroutine(CloseShieldEnergy());
    }

    IEnumerator CloseShieldEnergy()
    {
        yield return new WaitForSeconds(3.0f);
        shieldEnergy.gameObject.SetActive(false);
    }

    void ActiveInvArmor(Tank origin, ShellExplosion shell)
    {
        shieldInvert.gameObject.SetActive(true);

        shieldInvert.DOKill();
        shieldInvert.localScale = Vector3.one * 1.0f;
        shieldInvert.DOScale(3, 0.4f).SetEase(Ease.OutBounce).OnComplete(() => { shieldInvert.gameObject.SetActive(false); });

        var newShell = Instantiate(invShellPrefab, shell.transform.position, Quaternion.LookRotation(origin.transform.position - shell.transform.position), null);
        newShell.host = this.transform;
        newShell.origin = host;
        newShell.damage = shell.damage;
    }

    protected override void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        m_ExplosionParticles.Play();

        m_ExplosionAudio.Play();

        dieIntoParts?.Die();
        CombatSystem.instance.GameOver();
        //show fail
    }
}
