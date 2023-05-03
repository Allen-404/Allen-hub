using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroChooseItem : MonoBehaviour
{
    public Sprite myHeroSprite;
    public string lore;

    private void Start()
    {
        myHeroSprite = transform.GetChild(0).GetComponent<Image>().sprite;
    }

    public void OnClickHero()
    {
        ChooseHeroPageBehaviour.instance.OnChooseHero(this);
    }
}