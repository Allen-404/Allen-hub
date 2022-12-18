using UnityEngine;
using System.Collections;

public class PlayerTank : Tank
{
    [HideInInspector]
    public TankMovement movement;
    [HideInInspector]
    public TankShooting shooting;

    public float checkLaunchMineInterval = 1;

    protected override void Awake()
    {
        base.Awake();

        shooting = GetComponent<TankShooting>();
        movement = GetComponent<TankMovement>();

        movement.host = this;
        shooting.host = this;
    }

    private void Start()
    {
        StartCoroutine(CheckLaunchMine());
    }

    IEnumerator CheckLaunchMine()
    {
        yield return new WaitForSeconds(checkLaunchMineInterval);
        if (RoguelikeCombat.RoguelikeRewardSystem.instance.HasPerk(RoguelikeCombat.RoguelikeIdentifier.Mine))
        {
            LaunchMine();
        }

        StartCoroutine(CheckLaunchMine());
    }

    void LaunchMine()
    {
        var mine = Instantiate(CombatSystem.instance.minePrefab, transform.position - movement.transform.forward * 0.5f, Quaternion.identity, transform.parent);

    }
}
