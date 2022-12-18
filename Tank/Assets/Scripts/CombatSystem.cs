using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CombatSystem : MonoBehaviour
{
    public static CombatSystem instance;

    public GameObject vfx_constructionExplode;
    public GameObject vfx_mineExplode;
    public CanvasGroup cg_starting;
    public CanvasGroup cg_gameOver;

    public GameObject minePrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ShowStartingView();
    }

    void ShowStartingView()
    {
        cg_starting.alpha = 1;
        StartCoroutine(FadeStartingView());
    }

    IEnumerator FadeStartingView()
    {
        yield return new WaitForSeconds(2);
        cg_starting.DOFade(0, 1f);
        cg_starting.interactable = false;
        cg_starting.blocksRaycasts = false;
    }

    public void GameOver()
    {
        StartCoroutine(DoGameOver());
    }

    IEnumerator DoGameOver()
    {
        yield return new WaitForSeconds(2.5f);
        cg_gameOver.DOFade(1, 4f);
        cg_gameOver.interactable = true;
        cg_gameOver.blocksRaycasts = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }

    public void Win()
    {

    }
}
