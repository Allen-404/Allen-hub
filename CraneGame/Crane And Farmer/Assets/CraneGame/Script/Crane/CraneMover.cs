using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CraneMover : MonoBehaviour
{
    public NavMeshAgent nma;
    public Animator animator;

    public void SetDest(Vector3 pos)
    {
        nma.SetDestination(pos);
        ToggleWalk(true);
    }

    void ToggleWalk(bool b)
    {
        animator.SetBool("walk", b);
    }

    private void FixedUpdate()
    {
        if (nma.pathStatus == NavMeshPathStatus.PathComplete)
        {
            ToggleWalk(false);
        }
    }
}