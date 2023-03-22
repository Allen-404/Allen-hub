using UnityEngine;
using DG.Tweening;

public class WindmillRotateBehaviour : MonoBehaviour
{
    public Transform part;
    public Quaternion endQ;

    private void Start()
    {
        TurnOneRound();
    }

    void TurnOneRound()
    {
        part.DORotateQuaternion(endQ, 4).OnComplete(TurnOneRound);
    }
}
