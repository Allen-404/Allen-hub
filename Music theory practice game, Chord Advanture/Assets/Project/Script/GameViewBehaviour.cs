using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameViewBehaviour : MonoBehaviour
{
    public RectTransform heroImg;
    public RectTransform enemyImg;

    public float enemyEnterFromAnchoredX = 400;
    public float enemyEnterToAnchoredX = -50;
    public float enemyEnterDuration = 2;
    public float heroEnterFromAnchoredX = -400;
    public float heroEnterToAnchoredX = 80;
    public float heroEnterDuration = 2;
    public float enemyShowDelay = 0.5f;

    public float heroBaseAnchoredY = 0;
    public float heroJumpAnchoredY = 110;
    public ParticleSystem enemyShoutVfx;
}