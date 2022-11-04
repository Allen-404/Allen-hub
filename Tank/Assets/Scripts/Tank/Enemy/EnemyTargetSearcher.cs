using UnityEngine;
using System.Collections;
using com;

public class EnemyTargetSearcher : Ticker
{
    [HideInInspector]
    public EnemyTank host;

    public float sightRange;
    public float sightAngle;
    public bool alert { get; private set; }

    public Tank currentTarget { get; private set; }

    private void Start()
    {
        ExitAlert();
    }

    void CheckAlertState()
    {
        if (alert)
        {
            if (currentTarget == null || currentTarget.IsDead())
                ExitAlert();
        }
    }

    void ExitAlert()
    {
        //Debug.Log("ExitAlert");
        alert = false;
        currentTarget = null;
    }

    void EnterAlert(Tank target)
    {
        //Debug.Log("EnterAlert");
        alert = true;
        currentTarget = target;
    }

    void CheckSight()
    {
        if (alert)
            return;

        var targetInSight = FindTargetInSight();
        if (targetInSight != null)
            FoundTargetInSight(targetInSight);
    }

    Tank FindTargetInSight()
    {
        if (host.attackChecker.hasTargetInSight)
            return GameManager.instance.playerTank;
        return null;
    }

    void FoundTargetInSight(Tank target)
    {
        EnterAlert(target);
    }

    public void OnAttacked(Tank origin)
    {
        if (origin != null && !origin.IsDead())
        {
            EnterAlert(origin);
        }
    }

    protected override void Tick()
    {
        CheckAlertState();
        CheckSight();
        ChaseIfHasTarget();
    }

    void ChaseIfHasTarget()
    {
        if (alert && currentTarget != null && !currentTarget.IsDead())
        {
            host.movement.Chase();
        }
    }
}
