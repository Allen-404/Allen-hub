using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public EnemyTank host;

    NavMeshAgent _agent;

    public Transform[] patrolPoints;
    int _patrolIndex;
    bool _tempStop;
    public float tempStopDuration = 1.8f;
    float _resumeTempStopTimestamp;
    Vector3 _lastDestination;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //Debug.LogWarning(gameObject.GetHashCode());
        _patrolIndex = -1;
        Arrived();
    }

    private void Update()
    {
        if (!host.targetSearcher.alert)
        {
            CheckMove();
            return;
        }
    }

    private void CheckMove()
    {
        if (_tempStop)
        {
            if (Time.time > _resumeTempStopTimestamp)
            {
                _tempStop = false;
                SetDestination(_lastDestination);
            }

            return;
        }

        var distanceVector = transform.position - _agent.destination;
        distanceVector.y = 0;
        if (distanceVector.magnitude < 0.6f)
            Arrived();
    }

    public void StartTempStop()
    {
        _resumeTempStopTimestamp = Time.time + tempStopDuration;
        _tempStop = true;
        _lastDestination = _agent.destination;
        SetDestination(transform.position);
    }

    public void Chase()
    {
        SetDestination(host.targetSearcher.currentTarget.transform.position);
    }

    void Arrived()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        _patrolIndex = _patrolIndex + 1;
        if (_patrolIndex >= patrolPoints.Length)
            _patrolIndex = 0;

        var nextPoint = patrolPoints[_patrolIndex];
        SetDestination(nextPoint.position);
    }

    void SetDestination(Vector3 targetPosition)
    {
        _agent.SetDestination(targetPosition);
    }
}