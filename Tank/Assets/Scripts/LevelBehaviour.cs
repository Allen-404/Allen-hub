using UnityEngine;
using System.Collections;

public class LevelBehaviour : MonoBehaviour
{
    public GameObject[] DisableAfterWin;
    public GameObject[] enableAfterWin;

    public void Win()
    {
        foreach (var g in DisableAfterWin)
        {
            g.SetActive(false);
        }
        foreach (var g in enableAfterWin)
        {
            g.SetActive(true);
        }
    }
}
