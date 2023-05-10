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

    public TMPro.TextMeshProUGUI timingTxt;
    public Image heroImage;
    public ChooseHeroPageBehaviour chooseHeroPage;

    int crtLevelIndex;
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
        StartLevel(0);
    }

    public GameLevel GetCrtLevel()
    {
        return _crtLevel;
    }

    void PopText(string s)
    {
        timingTxt.text = s;

        timingTxt.DOKill();
        timingTxt.transform.localScale = Vector3.one * originalTimingTxtScale;
        timingTxt.transform.DOPunchScale(Vector3.one * originalTimingTxtScale * 1.1f, 0.4f, 4, 0.6f);
    }

    public void StartNextLevel()
    {
        StartLevel(crtLevelIndex + 1);
    }

    public void StartLevel(int index)
    {
        crtLevelIndex = index;
        var newLevel = levels[crtLevelIndex];
        if (newLevel == null)
        {
            Debug.LogWarning("no this level， index=" + index);
            return;
        }
        Debug.LogWarning("StartLevel index=" + index);
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
        SoundSystem.instance.Play("showup");
        var newPos1 = gameView.heroImg.anchoredPosition;
        newPos1.x = gameView.heroEnterFromAnchoredX;
        gameView.heroImg.anchoredPosition = newPos1;

        gameView.enemyImg.localRotation = Quaternion.identity;
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
            Debug.Log("monster: " + goal);
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
        //KeyboardBehaviour.instance.Clear();
        _restTime = _crtLevel.totalTime;
        PopText(Mathf.FloorToInt(_restTime) + "");
        while (_restTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            _restTime -= 1;
            PopText(Mathf.FloorToInt(_restTime) + "");
        }

        if (GameStateSystem.instance.state == GameState.Input_All)
        {
            PopText("Timeup!!!");
            yield return new WaitForSeconds(1.0f);
            Loose();
        }
        else
        {
            //do nothing because already win!
        }
        
    }

    IEnumerator Sequence_ShowResult_Win()
    {
        //monster shake and upsidedown
        //player jump
        //play sound
        //goto to level nextgameView.enemyImg.DOKill();
        gameView.enemyImg.DOShakePosition(1.0f, 35.0f, 8).OnComplete(
            () =>
            {
                gameView.enemyImg.DOLocalRotate(new Vector3(0, 0, -90), 0.7f);
                gameView.heroImg.DOAnchorPosY(gameView.heroJumpAnchoredY, 0.35f).SetEase(Ease.OutCubic).OnComplete(
                    () => { gameView.heroImg.DOAnchorPosY(gameView.heroBaseAnchoredY, 0.35f).SetEase(Ease.InCubic); }
                    );

            });

        SoundSystem.instance.Play("win");
        yield return new WaitForSeconds(1.75f);
        gameView.heroImg.DOKill();
        gameView.heroImg.DOAnchorPosY(gameView.heroJumpAnchoredY, 0.35f).SetEase(Ease.OutCubic).OnComplete(
                   () => { gameView.heroImg.DOAnchorPosY(gameView.heroBaseAnchoredY, 0.35f).SetEase(Ease.InCubic); }
                   );
        yield return new WaitForSeconds(1.0f);
        StartNextLevel();
    }

    IEnumerator Sequence_ShowResult_Loose()
    {
        SoundSystem.instance.Play("loose");
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

    public void Win()
    {
        _restTime = 0;
        Debug.LogWarning("Win!!!");
        GameStateSystem.instance.StartState(GameState.ShowResult);
        StartCoroutine(Sequence_ShowResult_Win());
    }

    public void Loose()
    {
        Debug.LogWarning("Loose!!!");
    }
}