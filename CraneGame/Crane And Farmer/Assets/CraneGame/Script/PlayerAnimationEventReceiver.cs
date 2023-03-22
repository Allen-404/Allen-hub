using UnityEngine;
using System.Collections;

public class PlayerAnimationEventReceiver : MonoBehaviour
{
    public PlayerPickBucket pickBucket;
    public PlayerOperateWell operateWell;

    public void Picked()
    {
        pickBucket.TryPick();
    }

    public void PickedEnd()
    {
        pickBucket.PickedEnd();
    }

    public void StartOperateWell()
    {
        operateWell.StartOperateWell();
    }

    public void EndOperateWell()
    {
        operateWell.EndOperateWell();
    }
}