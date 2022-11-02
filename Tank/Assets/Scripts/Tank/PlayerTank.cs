using UnityEngine;

public class PlayerTank : Tank
{
    [HideInInspector]
    public TankMovement movement;
    [HideInInspector]
    public TankShooting shooting;

    protected override void Awake()
    {
        base.Awake();

        shooting = GetComponent<TankShooting>();
        movement = GetComponent<TankMovement>();

        movement.host = this;
        shooting.host = this;
    }
}
