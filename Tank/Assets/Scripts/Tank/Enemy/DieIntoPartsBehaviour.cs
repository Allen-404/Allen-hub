using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DieIntoPartsBehaviour : MonoBehaviour
{
    public Collider[] parts;

    public void Die()
    {
        foreach (var col in parts)
        {
            col.transform.SetParent(null);

            col.enabled = true;
            var rb = col.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.mass = 1;
            rb.constraints = RigidbodyConstraints.None;
        }

        StartCoroutine(PostDie());
    }

    IEnumerator PostDie()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (var col in parts)
        {
            var rb = col.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(Random.Range(-200, 200), 350f, Random.Range(-200, 200)));
            var t = Random.Range(-500, 500);
            rb.AddTorque(new Vector3(t, t, t));
        }
        yield return new WaitForSeconds(2.4f);
        foreach (var col in parts)
        {
            col.transform.DOScale(0, 1f).OnComplete(() => { Destroy(col.gameObject); });
        }
    }
}