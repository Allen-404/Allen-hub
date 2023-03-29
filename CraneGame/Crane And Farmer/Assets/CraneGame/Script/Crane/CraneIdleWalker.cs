using UnityEngine;
using System.Collections;

public class CraneIdleWalker : MonoBehaviour
{
    public Transform[] destinations;

    private Transform crtDestination;

    public float nextWalkTimeMin = 6;
    public float nextWalkTimeMax = 10;
    float _nextWalkTime;
    CraneMover _mover;

    private void Start()
    {
        _mover = GetComponent<CraneMover>();
        _nextWalkTime = 3;
    }

    void SetNewDestination()
    {
        Transform newDestination;

        do { newDestination = GetRandomDestination(); }
        while (newDestination != crtDestination);

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
