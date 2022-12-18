using UnityEngine;
using System.Collections;

public class EnemyTankHealth : TankHealth
{
    public enum TankTier
    {
        None,
        Light,
        Heavy,
    }

    public TankTier tankTier;

    public override void TakeDamage(float amount, Tank origin, ShellExplosion shell = null)
    {
        if (RoguelikeCombat.RoguelikeRewardSystem.instance.HasPerk(RoguelikeCombat.RoguelikeIdentifier.AP))
        {
            if (tankTier == TankTier.Heavy)
            {
                amount *= 3;
            }
        }
        if (RoguelikeCombat.RoguelikeRewardSystem.instance.HasPerk(RoguelikeCombat.RoguelikeIdentifier.HE))
        {
            if (tankTier == TankTier.Light)
            {
                amount *= 3;
            }
        }

        base.TakeDamage(amount, origin);
        (host as EnemyTank).targetSearcher.OnAttacked(origin);
    }
}