using UnityEngine;
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
    public bool harmEnemy;
    public bool harmPlayer;
    private void Start()
    {
        _dieTimestamp = GameTime.time + m_MaxLifeTime;
    }

    private void Update()
    {
        if (GameTime.time > _dieTimestamp)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        //  Debug.Log(host.gameObject);
        if (other.transform == host)
            return;

        var willDestroy = false;
        ConstructionDestroyable constructionDestroyable = other.GetComponent<ConstructionDestroyable>();
        if (constructionDestroyable != null)
        {
            willDestroy = true;
            if (harmEnemy)
            {
                constructionDestroyable.ReceiveDamage((int)damage);
            }
        }

        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);


        for (int i = 0; i < colliders.Length; i++)
        {
            //Debug.Log("colliders" + colliders[i].gameObject);
            if (colliders[i].transform == host)
                continue;

            var landMine = colliders[i].transform.GetComponent<LandMineBehaviour>();
            if (landMine != null)
            {
                landMine.ExplodeWithDelay(0.25f);
                willDestroy = true;
                break;
            }

            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;


            Tank tank = targetRigidbody.GetComponent<Tank>();

            if (tank != null)
            {
                if (tank == origin)
                {
                    continue;
                }
                else if ((tank.identifier == TankIdentifier.Player && harmPlayer) || (tank.identifier == TankIdentifier.Enemy && harmEnemy))
                {
                    willDestroy = true;
                    tank.health.TakeDamage(damage, origin, this);
                    targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
                }
            }
        }

        if (willDestroy)
        {
            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy(m_ExplosionParticles.gameObject, 3);
            Destroy(gameObject);
        }
    }
}