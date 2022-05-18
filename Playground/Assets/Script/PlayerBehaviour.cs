using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform destination;

    public NavMeshAgent agent;

    private float _timer;
    public float tickTime = 1;

    public GameObject prefabDot;

    private List<GameObject> _currentDots;
    private Vector3 _lastPos;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        _currentDots = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            SetNewDestination();
            _timer = tickTime;
        }
    }

    void SetNewDestination()
    {
        if (_lastPos == destination.position)
        {
            return;
        }


        foreach (var oldDot in _currentDots)
        {
            Destroy(oldDot);
        }
        _currentDots = new List<GameObject>();

        //function
        //method
        //API application programming interface
        agent.SetDestination(destination.position);
        _lastPos = destination.position;

        var dots = agent.path;
        foreach(var dot in dots.corners)
        {
          var newDot =   Instantiate(prefabDot, dot + Vector3.up * 0.7f, Quaternion.identity, transform.parent);
            _currentDots.Add(newDot);
        }
        //define
        //implement
    }
}
