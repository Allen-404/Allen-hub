using UnityEngine;

public class EnemyTank : Tank
{
    public EnemyAttackChecker attackChecker;
    [HideInInspector]
    public EnemyMovement movement;
    [HideInInspector]
    public EnemyShooting shooting;
    [HideInInspector]
    public EnemyTargetSearcher targetSearcher;

    protected override void Awake()
    {
        base.Awake();

        movement = GetComponent<EnemyMovement>();
        shooting = GetComponent<EnemyShooting>();
        targetSearcher = GetComponent<EnemyTargetSearcher>();

        attackChecker.host = this;
        movement.host = this;
        shooting.host = this;
        targetSearcher.host = this;
    }
}
