using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public List<GameLevel> levels;
    private GameLevel _crtLevel;
    private GameViewBehaviour gameView;
    private int _restTime;
    public int totalInputAllTime = 10;

    public TMPro.TextMeshProUGUI timingTxt;
    public Image heroImage;
    public ChooseHeroPageBehaviour chooseHeroPage;

    float originalTimingTxtScale;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameView = GetComponent<GameViewBehaviour>();
        chooseHeroPage.gameObject.SetActive(true);
        originalTimingTxtScale = timingTxt.transform.localScale.x;
    }

    public void SetHeroSprite(Sprite sp)
    {
        heroImage.sprite = sp;
    }

    public void StartGame()
    {
        StartNewLevel(levels[0]);
    }

    void PopText(string s)
    {
        timingTxt.text = s;

        timingTxt.DOKill();
        timingTxt.transform.localScale = Vector3.one * originalTimingTxtScale;
        timingTxt.transform.DOPunchScale(Vector3.one * originalTimingTxtScale * 1.1f, 0.4f, 4, 0.6f);
    }

    public void StartNewLevel(GameLevel newLevel)
    {
        timingTxt.text = "";
        _crtLevel = newLevel;
        gameView.enemyImg.GetComponent<Image>().sprite = _crtLevel.enemySprite;
        GameStateSystem.instance.StartState(GameState.ShowUp);
    }

    public void Start_Showup()
    {
        PopText("The monster is shouting out!");
        StartCoroutine(Sequence_ShowUp());
    }

    public void Start_Listen_All()
    {
        StartCoroutine(Sequence_Listen_All());
    }

    public void Start_Input_All()
    {
        StartCoroutine(Sequence_Input());
    }

    IEnumerator Sequence_ShowUp()
    {
        KeyboardBehaviour.instance.Clear();

        var newPos1 = gameView.heroImg.anchoredPosition;
        newPos1.x = gameView.heroEnterFromAnchoredX;
        gameView.heroImg.anchoredPosition = newPos1;

        var newPos2 = gameView.enemyImg.anchoredPosition;
        newPos2.x = gameView.enemyEnterFromAnchoredX;
        gameView.enemyImg.anchoredPosition = newPos2;

        gameView.heroImg.DOAnchorPosX(gameView.heroEnterToAnchoredX, gameView.heroEnterDuration).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(gameView.enemyShowDelay);
        gameView.enemyImg.DOAnchorPosX(gameView.enemyEnterToAnchoredX, gameView.enemyEnterDuration).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(0.4f);
        PopText("Enter the notes!");
        yield return new WaitForSeconds(0.8f);
        GameStateSystem.instance.GoNextState();
    }

    IEnumerator Sequence_Listen_All()
    {
        KeyboardBehaviour.instance.Clear();
        yield return new WaitForSeconds(1.0f);
        //PlayLevelSfxs//播放本关的怪物的声音
        foreach (var goal in _crtLevel.goals)
        {
            KeyboardBehaviour.PlayNoteSound(goal);
            PlayMonsterFeedback();
            if (_crtLevel.interval > 0)
            {
                yield return new WaitForSeconds(_crtLevel.interval);
            }
        }
        yield return new WaitForSeconds(1.5f);
        GameStateSystem.instance.GoNextState();
    }

    void PlayMonsterFeedback()
    {
        gameView.enemyImg.DOKill();
        gameView.enemyImg.DOShakePosition(0.8f, 50.0f, 8);

        gameView.enemyShoutVfx.Stop();
        gameView.enemyShoutVfx.Play();
    }

    IEnumerator Sequence_Input()
    {
        KeyboardBehaviour.instance.Clear();
        _restTime = totalInputAllTime;
        PopText(Mathf.FloorToInt(_restTime) + "");
        while (_restTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            _restTime -= 1;
            PopText(Mathf.FloorToInt(_restTime) + "");
        }
        PopText("Timeup!!!");
        yield return new WaitForSeconds(1.0f);
        GameStateSystem.instance.GoNextState();
    }

    IEnumerator Sequence_ShowResult()
    {
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator Sequence_Sub_Listen_1Note()
    {
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator Sequence_Sub_Input_1Note()
    {
        yield return new WaitForSeconds(1.0f);
    }
}