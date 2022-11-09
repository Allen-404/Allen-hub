using UnityEngine;
using System.Collections.Generic;

public class EnemyAttackChecker : MonoBehaviour
{
    [HideInInspector]
    public EnemyTank host;

    public bool hasTargetInSight { get; private set; }

    static List<EnemyAttackChecker> selves = new List<EnemyAttackChecker>();

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Tank>();
        if (player == GameManager.instance.playerTank)
        {
            Debug.Log("player in sight");
            hasTargetInSight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.gameObject.GetComponent<Tank>();
        if (player == GameManager.instance.playerTank)
        {
            Debug.Log("player no in sight");
            hasTargetInSight = false;
        }
    }

    void Awake()
    {
        hasTargetInSight = false;
        selves.Add(this);
    }

    void OnDisable()
    {
        selves.Remove(this);
    }
}
