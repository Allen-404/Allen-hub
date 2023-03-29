using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CraneSystem : MonoBehaviour
{
    public static CraneSystem instance;

    public CraneAttributeBehaviour satiety;
    public CraneAttributeBehaviour thirsty;

    public float tickTime = 1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(TickCoroutine());
    }

    IEnumerator TickCoroutine()
    {
        yield return new WaitForSeconds(tickTime);
        Tick();
        StartCoroutine(TickCoroutine());
    }

    void Tick()
    {
        satiety.Tick();
        thirsty.Tick();
    }
}
