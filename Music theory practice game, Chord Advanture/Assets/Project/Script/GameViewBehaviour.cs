using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
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
    public float enemyBaseAnchoredY = 0;
    public float enemyJumpAnchoredY = 110;
    public ParticleSystem enemyShoutVfx;
    public CanvasGroup totalFailureView;
    public CanvasGroup descView;
    public TextMeshProUGUI descTxt;
    public GameObject readyView;
}