using UnityEngine;
using System.Collections;

public class CombatSystem : MonoBehaviour
{
    public static CombatSystem instance;

    public GameObject vfx_constructionExplode;

    private void Awake()
    {
        instance = this;
    }
}
