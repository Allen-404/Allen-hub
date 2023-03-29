using UnityEngine;
using System.Collections;

public class PlayerPickBucket : MonoBehaviour
{
    public Collider col;
    public BucketBehaviour bucketInHand;
    public AudioSource sfxPickup;
    public AudioSource sfxDrop;

    private void Start()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter " + other.gameObject.name);

        var bucket = other.GetComponent<BucketBehaviour>();
        if (bucketInHand == null && bucket != null)
        {

            bucket.OnPicked();
            bucketInHand = bucket;
            col.enabled = false;
            sfxPickup.Play();
            return;
        }

        if (bucketInHand != null && (bucket == bucketInHand || bucket == null))
        {
            bucketInHand.OnDroped();
            bucketInHand = null;
            col.enabled = false;
            sfxDrop.Play();
        }
    }

    public void TryPick()
    {
        col.enabled = true;
    }
    public void PickedEnd()
    {
        col.enabled = false;
    }
}