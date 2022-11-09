using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnSystem : MonoBehaviour
{
    public List<EnemySpawnInfo> spawnInfos;

    float _nextSpawnTimestamp;
    int _index;
    bool _isSpawning;

   public  List<EnemyTank> enemies { get; private set; }

    private void Start()
    {
        _index = 0;
        _nextSpawnTimestamp = 0;
        _isSpawning = true;
        enemies = new List<EnemyTank>();
    }

    public bool HasEndedSpawn()
    {
        return !_isSpawning;
    }

    public bool HasAliveEnemy()
    {
        foreach (var ene in enemies)
        {
            if (ene != null && !ene.IsDead())
                return true;
        }

        return false;
    }

    public void Spawn()
    {
        var currentInfo = spawnInfos[_index];
        if (currentInfo.prefab != null)
        {
            var tank = Instantiate(currentInfo.prefab, currentInfo.spawnPlace.position, currentInfo.spawnPlace.rotation);
            tank.movement.patrolPoints = currentInfo.patrolPoints;
            enemies.Add(tank);
        }

        _nextSpawnTimestamp = Time.time + currentInfo.delayToNext;

        _index++;
        if (_index >= spawnInfos.Count)
            _isSpawning = false;
    }

    private void Update()
    {
        CheckSpawnProcess();
    }

    void CheckSpawnProcess()
    {
        if (!_isSpawning)
            return;

        if (_nextSpawnTimestamp > Time.time)
            return;

        Spawn();
    }

    [System.Serializable]
    public class EnemySpawnInfo
    {
        public EnemyTank prefab;
        public float delayToNext;
        public Transform spawnPlace;
        public Transform[] patrolPoints;
    }
}