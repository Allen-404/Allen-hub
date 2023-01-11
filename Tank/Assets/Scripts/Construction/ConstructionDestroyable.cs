using UnityEngine;


[System.Serializable]
public class GuardianEnemySpawnInfo
{
    public EnemyTank[] prefabs_HpAboveHalf;
    public EnemyTank[] prefabs_HpBelowHalf;
    public int spawnHpLoss;
    public Transform spawnPlace;
    public Transform[] patrolPoints;
}

public class ConstructionDestroyable : MonoBehaviour
{
    public int hpMax;
    int _hp;

    public HpBar hpbarPrefab;
    HpBar _bar;

    bool _dead;
    public GameObject explodePrefab;
    public float hpBarHeight = 4;
    public float hpBarScale = 0.01f;
    public bool startRoguelikeUpgrade;
    public GuardianEnemySpawnInfo guardianEnemySpawnInfo;
    public LevelBehaviour winLevelAfterDestroyed;
    private int _hpLoss;

    void Start()
    {
        _hp = hpMax;
        _hpLoss = 0;
        _dead = false;
    }

    public void ReceiveDamage(int dmg)
    {
        if (_dead)
            return;

        _hp -= dmg;
        _hpLoss += dmg;
        TrySpawnGuardianEnemies();

        if (_hp <= 0)
        {
            Die();
        }
        else
        {
            SyncBar();
        }
    }

    void TrySpawnGuardianEnemies()
    {
        var info = guardianEnemySpawnInfo;
        if (info == null)
            return;
        if (info.spawnHpLoss <= 0)
            return;
        if (info.spawnPlace == null)
            return;
        if (_hpLoss < info.spawnHpLoss)
            return;

        _hpLoss -= info.spawnHpLoss;
        var hpRatio = (float)_hp / hpMax;

        EnemyTank finalPrefab = null;
        if (hpRatio > 0.5f)
            finalPrefab = info.prefabs_HpAboveHalf[Random.Range(0, info.prefabs_HpAboveHalf.Length)];
        else
            finalPrefab = info.prefabs_HpBelowHalf[Random.Range(0, info.prefabs_HpBelowHalf.Length)];

        if (finalPrefab == null)
            return;

        var tank = Instantiate(finalPrefab, info.spawnPlace.position, info.spawnPlace.rotation);
        tank.movement.patrolPoints = info.patrolPoints;
        EnemySpawnSystem.instance.enemies.Add(tank);
    }

    void Die()
    {
        if (_dead)
            return;
        _dead = true;
        if (_bar != null)
            Destroy(_bar.gameObject);

        var dieVFx = Instantiate(explodePrefab, transform.position + Vector3.up, transform.rotation);
        Destroy(dieVFx, 3);
        var dieVFx2 = Instantiate(CombatSystem.instance.vfx_constructionExplode, transform.position + Vector3.up, transform.rotation);
        Destroy(dieVFx2, 3);

        winLevelAfterDestroyed?.Win();

        if (startRoguelikeUpgrade)
            RoguelikeCombat.RoguelikeRewardSystem.instance.StartNewEventWithDelay(3);

        Destroy(gameObject);
    }


    void SyncBar()
    {
        if (_bar == null)
        {
            _bar = Instantiate(hpbarPrefab, transform.position + Vector3.up * hpBarHeight, Quaternion.identity);
            _bar.gameObject.transform.localScale = Vector3.one * hpBarScale;
        }

        _bar.SetValue(((float)_hp / hpMax));
    }
}
