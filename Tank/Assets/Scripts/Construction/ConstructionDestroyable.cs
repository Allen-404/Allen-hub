using UnityEngine;

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

    public LevelBehaviour winLevelAfterDestroyed;

    void Start()
    {
        _hp = hpMax;
        _dead = false;
    }

    public void ReceiveDamage(int dmg)
    {
        if (_dead)
            return;

        _hp -= dmg;
        if (_hp <= 0)
        {
            Die();

        }
        else
        {
            SyncBar();
        }
    }

    void Die()
    {
        if (_dead)
            return;
        _dead = true;
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
