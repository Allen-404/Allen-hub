﻿using UnityEngine;
using com;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;
    public AudioSource m_ExplosionAudio;
    public float damage = 100f;
    public float m_ExplosionForce = 1000f;
    public float m_MaxLifeTime = 2f;
    public float m_ExplosionRadius = 5f;
    public Tank origin;
    public Transform host;
    private float _dieTimestamp;

    private void Start()
    {
        _dieTimestamp = GameTime.time + m_MaxLifeTime;
    }

    private void Update()
    {
        if(GameTime.time> _dieTimestamp)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        //  Debug.Log(host.gameObject);
        if (other.transform == host)
            return;

        ConstructionDestroyable constructionDestroyable = other.GetComponent<ConstructionDestroyable>();
        if (constructionDestroyable != null)
        {
            constructionDestroyable.ReceiveDamage((int)damage);
        }

        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform == host)
                continue;

            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            if (!targetHealth)
                continue;

            float damage = CalculateDamage(targetRigidbody.position);
            targetHealth.TakeDamage(damage,origin);
        }

        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();
        Destroy(m_ExplosionParticles.gameObject, 3);
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}