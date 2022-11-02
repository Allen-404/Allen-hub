using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [HideInInspector]
    public EnemyTank host;

    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public AudioSource m_ShootingAudio;
    public AudioClip m_ChargingClip;
    public AudioClip m_FireClip;
    public int baseDamage;
    public float attackRate;
    float _attackIntervalTimer;

    private void Update()
    {
        if (host.attackChecker.hasTargetInSight)
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (_attackIntervalTimer <= 0)
        {
            Fire();
            _attackIntervalTimer = attackRate;
        }
        else
        {
            _attackIntervalTimer -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        shellInstance.GetComponent<ShellExplosion>().origin = host;
        var finalDamage = baseDamage;
        CreateBullet(finalDamage, m_FireTransform.position);
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

    void CreateBullet(int dmg, Vector3 pos)
    {
        var shell = Instantiate(m_Shell, pos, m_FireTransform.rotation);
        ShellExplosion se = shell.GetComponent<ShellExplosion>();
        se.host = this.transform;
        se.origin = host;
        se.damage = dmg;
    }
}