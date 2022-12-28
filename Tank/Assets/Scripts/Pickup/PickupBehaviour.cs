using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    public enum PickType
    {
        None = 0,
        Heal100Percent = 1,
    }

    public PickType type;
    public GameObject vfxPrefab;

    private void OnTriggerEnter(Collider other)
    {
        Tank tank = other.GetComponent<Tank>();

        if (tank != null && tank.identifier == TankIdentifier.Player)
        {
            switch (type)
            {
                case PickType.None:
                    break;

                case PickType.Heal100Percent:
                    tank.health.Heal(99999);
                    break;
            }

            if (vfxPrefab != null)
            {
                var vfx = Instantiate(vfxPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity, null);
                Destroy(vfx, 3);
            }

            Destroy(gameObject);
        }
    }
}