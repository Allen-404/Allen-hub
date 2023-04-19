using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CraneMover : MonoBehaviour
{
    public NavMeshAgent nma;
    public Animator animator;
    public Animator[] animators;
    Vector3 _targetPos;

    public void SetDest(Vector3 pos)
    {
        _targetPos = pos;
        nma.SetDestination(pos);
        ToggleWalk(true);
    }

    void ToggleWalk(bool b)
    {
        animator.SetBool("walk", b);
        if (animators != null)
        {
            foreach (var a in animators)
            {
                a.SetBool("walk", b);
            }
        }
    }

    private void FixedUpdate()
    {
        var dist = _targetPos - transform.position;
        dist.y = 0;
        if (dist.magnitude < 0.5f)
        {
            ToggleWalk(false);
        }
    }
}