using UnityEngine;
using DG.Tweening;
using com;

public class LandMineBehaviour : MonoBehaviour
{
    public Animator animator;

    bool _isGoingToExplode;
    public float explodeDelay = 0.5f;
    float _explodeDelayTimer;

    public float safeExplodeDelay = 15f;
    float _safeExplodeDelayTimer;
    public LayerMask tankMask;
    public float activateRadius = 2;
    public float explosionRadius = 2;
    public float explosionForce;
    public int damage;

    void Start()
    {
        _isGoingToExplode = false;
        _safeExplodeDelayTimer = safeExplodeDelay;

        transform.localScale = Vector3.one * 0.3f;
        transform.DOScale(1, 0.9f).SetEase(Ease.OutBounce).OnComplete(
            () =>
            {
                animator.SetTrigger("active");
            }
            );
    }

    // Update is called once per frame
    void Update()
    {
        _safeExplodeDelayTimer -= GameTime.deltaTime;
        if (_safeExplodeDelayTimer < 0)
        {
            Explode();
            return;
        }

        if (_isGoingToExplode)
        {
            _explodeDelayTimer -= GameTime.deltaTime;
            if (_explodeDelayTimer < 0)
            {
                Explode();
            }
            return;
        }

        CheckEnemyNearby();
    }

    void CheckEnemyNearby()
    {
        bool hasAnyEnemyNearby = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, activateRadius, tankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Tank tank = colliders[i].GetComponent<Tank>();
            if (tank != null)
            {
                if (tank.identifier != TankIdentifier.Player)
                {
                    hasAnyEnemyNearby = true;
                    break;
                }
            }
        }


        if (hasAnyEnemyNearby)
        {
            _isGoingToExplode = true;
            _explodeDelayTimer = explodeDelay;
        }
    }

    public void ExplodeWithDelay(float delay)
    {
        if (_isGoingToExplode)
        {
            return;
        }
        _isGoingToExplode = true;
        _explodeDelayTimer = delay;
    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            var landMine = colliders[i].transform.GetComponent<LandMineBehaviour>();
            if (landMine != null)
            {
                landMine.ExplodeWithDelay(0.25f);
                continue;
            }

            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;
            Tank tank = targetRigidbody.GetComponent<Tank>();
            if (tank != null)
            {
                if (tank.identifier == TankIdentifier.Enemy)
                {
                    tank.health.TakeDamage(damage, null);
                    targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

        var vfx = Instantiate(CombatSystem.instance.vfx_mineExplode, transform.position, Quaternion.identity, null);
        //TODO sound
        Destroy(vfx, 3);
        Destroy(gameObject);
    }
}
