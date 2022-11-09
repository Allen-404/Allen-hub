using UnityEngine;
using System.Collections;

public class PlayerTankHealth : TankHealth
{
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
