using UnityEngine;
using System.Collections;

public class BucketBehaviour : MonoBehaviour
{
    public GameObject waterMesh;
    public Transform parentFarmerHand;
    public Transform parentDefault;

    public Collider col;

    public bool hasWater { get { return waterMesh.activeSelf; } }

    public void Start()
    {
        OnEmpty();
    }

    public void OnPicked()
    {
        col.enabled = false;
        transform.SetParent(parentFarmerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void OnDroped()
    {
        transform.SetParent(parentDefault);
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;
        transform.rotation = Quaternion.identity;
        col.enabled = true;
    }

    public void OnFullfilled()
    {
        waterMesh.SetActive(true);
    }

    public void OnEmpty()
    {
        waterMesh.SetActive(false);
    }
}