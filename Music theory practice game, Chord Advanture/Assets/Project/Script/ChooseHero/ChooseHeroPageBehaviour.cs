using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ChooseHeroPageBehaviour : MonoBehaviour
{
    public static ChooseHeroPageBehaviour instance;
    public TMPro.TextMeshProUGUI loreText;
    public GameObject confirmButton;

    private void Awake()
    {
        instance = this;
        loreText.text = "Please select your hero!";
        confirmButton.SetActive(false);
    }

    public void OnChooseHero(HeroChooseItem item)
    {
        GameSystem.instance.SetHeroSprite(item.myHeroSprite);
        loreText.text = item.lore;
        confirmButton.SetActive(true);
    }

    public void OnClickConfirm()
    {
        this.gameObject.SetActive(false);
        GameSystem.instance.PrepareGame();
    }
}