using System;
using UnityEngine;

[Serializable]
public class TankColorAssigner : MonoBehaviour
{
    public Color color;

    public void Start()
    {
        SetColor();
    }

    public void SetColor()
    {
        MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var r in renderers)
        {
            r.material.color = color;
        }
    }
}
