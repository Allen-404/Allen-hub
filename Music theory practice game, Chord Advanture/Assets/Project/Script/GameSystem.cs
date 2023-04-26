using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameSystem : MonoBehaviour
{
    public List<GameLevel> levels;

    private GameLevel _crtLevel;
    private GameViewBehaviour gameView;

    private void Start()
    {
        gameView = GetComponent<GameViewBehaviour>();
    }

    public void TestLevel1()
    {
        StartNewLevel(levels[0]);
    }

    public void StartNewLevel(GameLevel newLevel)
    {
        _crtLevel = newLevel;
        gameView.enemyImg.GetComponent<Image>().sprite = _crtLevel.enemySprite;
        StartCoroutine(StartLevelSequence());
    }

    IEnumerator StartLevelSequence()
    {
        var newPos1 = gameView.heroImg.anchoredPosition;
        newPos1.x = gameView.heroEnterFromAnchoredX;
        gameView.heroImg.anchoredPosition = newPos1;

        var newPos2 = gameView.enemyImg.anchoredPosition;
        newPos2.x = gameView.enemyEnterFromAnchoredX;
        gameView.enemyImg.anchoredPosition = newPos2;

        gameView.heroImg.DOAnchorPosX(gameView.heroEnterToAnchoredX, gameView.heroEnterDuration).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(gameView.enemyShowDelay);
        gameView.enemyImg.DOAnchorPosX(gameView.enemyEnterToAnchoredX, gameView.enemyEnterDuration).SetEase(Ease.OutBack);
    }
}