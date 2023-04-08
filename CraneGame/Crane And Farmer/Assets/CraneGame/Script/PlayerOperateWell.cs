using UnityEngine;
using System.Collections;

public class PlayerOperateWell : MonoBehaviour
{
    public Collider col;
    public PlayerPickBucket pickBucket;
    public AudioSource sfxOperateWell;

    private void Start()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter " + other.gameObject.name);
        var well = other.GetComponent<WellBehaviour>();
        if (well != null)
        {

            well.Operate();
            if (pickBucket.bucketInHand!=null)
            {
                pickBucket.bucketInHand.OnFullfilled();
                sfxOperateWell.Play();
            }

            col.enabled = false;
            return;
        }
    }

    public void StartOperateWell()
    {
        col.enabled = true;
    }

    public void EndOperateWell()
    {
        col.enabled = false;
    }
}