﻿using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    [HideInInspector]
    public Tank host;

    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplosionPrefab;

    protected AudioSource m_ExplosionAudio;
    protected ParticleSystem m_ExplosionParticles;
    protected float m_CurrentHealth;
    protected bool m_Dead;
    public DieIntoPartsBehaviour dieIntoParts;
    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }


    public void TakeDamage(float amount, Tank origin)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        m_CurrentHealth -= amount;

        SetHealthUI();

        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
            return;
        }

        var enemyTank = host as EnemyTank;
        if (enemyTank != null)
        {
            enemyTank.targetSearcher.OnAttacked(origin);
        }
    }


    private void SetHealthUI()
    {
        m_Slider.value = m_CurrentHealth / m_StartingHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    protected virtual void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position+Vector3.up*0.6f;
        m_ExplosionParticles.gameObject.SetActive(true);

        m_ExplosionParticles.Play();

        m_ExplosionAudio.Play();

        //EnemyAttackChecker.CheckAllCheckers();
        //CheckWinSystem.instance.Check();
        dieIntoParts?.Die();
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return m_Dead;
    }
}