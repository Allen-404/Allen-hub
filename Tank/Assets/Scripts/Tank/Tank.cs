using UnityEngine;

public enum TankIdentifier
{
    Player,
    Wingman,
    Enemy,
}

public class Tank : MonoBehaviour
{
    public TankHealth health { get; private set; }
    public TankIdentifier identifier;

    protected virtual void Awake()
    {
        health = GetComponent<TankHealth>();
        health.host = this;
    }

    public virtual bool IsDead()
    {
        return health.IsDead();
    }
}