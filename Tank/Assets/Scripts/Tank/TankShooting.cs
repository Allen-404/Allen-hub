using UnityEngine;
using RoguelikeCombat;
using com;
using System.Collections;

public class TankShooting : MonoBehaviour
{
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public AudioSource m_ShootingAudio;
    public AudioClip m_FireClip;
    public float fireInverval = 0.4f;
    float _firedTimestamp;
    bool _isFiring;
    public int baseDamage = 20;

    [HideInInspector]
    public Tank host;

    private void Start()
    {
        _isFiring = false;
    }

    private void Update()
    {
        if (host.IsDead())
        {
            return;
        }
        if (com.GameTime.timeScale == 0)
        {
            _isFiring = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            _isFiring = true;
        if (Input.GetKeyUp(KeyCode.Space))
            _isFiring = false;

        if (_isFiring)
        {
            if (com.GameTime.time - _firedTimestamp > fireInverval)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        _firedTimestamp = com.GameTime.time;
        MakeFire();

        CoolDownSystem.instance.TryTrigger(RoguelikeIdentifier.RunningFire1,
            () => { StartCoroutine(MakeDelayedFire(0.11f)); }, 4.0f);
        CoolDownSystem.instance.TryTrigger(RoguelikeIdentifier.RunningFire2,
            () => { StartCoroutine(MakeDelayedFire(0.22f)); }, 4.0f);
    }

    IEnumerator MakeDelayedFire(float delay)
    {
        yield return new WaitForSeconds(delay);
        MakeFire();
    }

    void MakeFire()
    {
        var finalDamage = baseDamage;
        if (RoguelikeRewardSystem.instance.HasPerk(RoguelikeIdentifier.DamageUp1))
        {
            finalDamage = MathGame.GetPercentageAdded(finalDamage, 50);
        }
        if (RoguelikeRewardSystem.instance.HasPerk(RoguelikeIdentifier.DamageUp2))
        {
            finalDamage = MathGame.GetPercentageAdded(finalDamage, 50);
        }

        if (RoguelikeRewardSystem.instance.HasPerk(RoguelikeIdentifier.ExtraCannon1))
        {
            if (RoguelikeRewardSystem.instance.HasPerk(RoguelikeIdentifier.ExtraCannon2))
            {
                //3 shoots
                CreateBullet(finalDamage, m_FireTransform.position + transform.right * 0.3f);
                CreateBullet(finalDamage, m_FireTransform.position);
                CreateBullet(finalDamage, m_FireTransform.position + transform.right * (-0.3f));
            }
            else
            {
                //2 shoots
                CreateBullet(finalDamage, m_FireTransform.position + transform.right * 0.2f);
                CreateBullet(finalDamage, m_FireTransform.position + transform.right * (-0.2f));
            }
        }
        else
        {
            //only 1 shoot
            CreateBullet(finalDamage, m_FireTransform.position);
        }

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