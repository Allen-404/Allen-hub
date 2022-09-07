using UnityEngine;
using System.Collections;

public class ConstructionDestroyable : MonoBehaviour
{
    public int hpMax;
    int _hp;

    public HpBar hpbarPrefab;
    HpBar _bar;

    bool _dead;
    public GameObject explodePrefab;

    void Start()
    {
        _hp = hpMax;
        _dead = false;
    }

    public void ReceiveDamage(int dmg)
    {
        if (_dead)
        {
            return;
        }

        _hp -= dmg;
        if (_hp <= 0)
        {
            Die();
            Destroy(_bar.gameObject);
        }
        else
        {
            SyncBar();
        }
    }

    void Die()
    {
        _dead = true;
        Destroy(gameObject, 2);
        var dieVFx = Instantiate(explodePrefab, transform.position + Vector3.up, transform.rotation);
        Destroy(dieVFx, 3);
    }

    void SyncBar()
    {
        if (_bar == null)
        {
            _bar = Instantiate(hpbarPrefab, transform.position + Vector3.up * 3, Quaternion.identity);
        }

        _bar.SetValue(((float)_hp / hpMax));
    }
}
