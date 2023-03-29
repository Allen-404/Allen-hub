using UnityEngine;
using System.Collections;

public class CraneIdleWalker : MonoBehaviour
{
    public Transform[] destinations;

    private Transform crtDestination;

    public float nextWalkTimeMin = 6;
    public float nextWalkTimeMax = 10;
    float _nextWalkTime;
    [SerializeField]
    CraneMover _mover;

    private void Start()
    {
        _nextWalkTime = 3;
    }

    void SetNewDestination()
    {
        Transform newDestination = null;

        int safeTime = 40;
        do
        {
            newDestination = GetRandomDestination();
            safeTime--;
        }
        while ((newDestination == null || newDestination == crtDestination) && safeTime > 0);
        crtDestination = newDestination;
        _nextWalkTime = Random.Range(nextWalkTimeMin, nextWalkTimeMax);
        _mover.SetDest(crtDestination.transform.position);
    }

    Transform GetRandomDestination()
    {
        return destinations[Random.Range(0, destinations.Length)];
    }

    private void Update()
    {
        _nextWalkTime -= Time.deltaTime;
        if (_nextWalkTime < 0)
        {
            SetNewDestination();
        }
    }
}
