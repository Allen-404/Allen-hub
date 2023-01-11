using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [HideInInspector]
    public EnemyTank host;
    public Rigidbody m_Shell;
    public Transform[] m_FireTransforms;
    public AudioSource m_ShootingAudio;
    public AudioClip m_FireClip;
    public int baseDamage;
    public float attackRate;
    float _attackIntervalTimer;

    private void Update()
    {
        if (com.GameTime.timeScale == 0)
        {
            return;
        }
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

    void Fire()
    {
        foreach (var cannon in m_FireTransforms)
        {
            FireByCannon(cannon);
        }

        host.movement.StartTempStop();
    }

    void FireByCannon(Transform cannonTrans)
    {
        Rigidbody shellInstance = Instantiate(m_Shell, cannonTrans.position, cannonTrans.rotation) as Rigidbody;
        shellInstance.GetComponent<ShellExplosion>().origin = host;
        var finalDamage = baseDamage;
        CreateBullet(finalDamage, cannonTrans.position, cannonTrans.rotation);
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

    void CreateBullet(int dmg, Vector3 pos, Quaternion rot)
    {
        var shell = Instantiate(m_Shell, pos, rot);
        ShellExplosion se = shell.GetComponent<ShellExplosion>();
        se.host = this.transform;
        se.origin = host;
        se.damage = dmg;
    }
}