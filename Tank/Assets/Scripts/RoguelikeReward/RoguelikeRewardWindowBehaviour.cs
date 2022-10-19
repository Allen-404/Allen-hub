using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using com;

namespace RoguelikeCombat
{
    public class RoguelikeRewardWindowBehaviour : MonoBehaviour
    {
        public static RoguelikeRewardWindowBehaviour instance;
        public CanvasGroup cg;

        RoguelikeRewardEventData _data;

        public TMPro.TextMeshProUGUI mainTitle;
        public TMPro.TextMeshProUGUI detailTitle;
        public TMPro.TextMeshProUGUI detailDesc;
        public Image detailImage;
        public GameObject button;
        public GameObject detailPanel;
        public List<RoguelikeRewardSlotBehaviour> slots;
        RoguelikeIdentifier _tempRoguelikeIdentifier;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Hide();
        }

        public void Setup(RoguelikeRewardEventData data)
        {
            _tempRoguelikeIdentifier = RoguelikeIdentifier.None;
            _data = data;

            button.SetActive(false);
            detailPanel.SetActive(false);
            //mainTitle.text = data.title;

            int lenSlot = slots.Count;
            int lenReward = data.rewards.Count;
            for (int i = 0; i < lenSlot; i++)
            {
                Debug.Log(i);
                var slot = slots[i];
                if (i >= lenReward)
                {
                    slot.Hide();
                    continue;
                }

                var reward = data.rewards[i];
                if (reward == RoguelikeIdentifier.None)
                {
                    slot.Hide();
                }
                else
                {
                    slot.Show(reward);
                }
            }
        }

        public void ShowDetail(RoguelikeRewardPrototype proto)
        {
            _tempRoguelikeIdentifier = proto.id;
            button.SetActive(true);

            detailTitle.text = proto.title;
            detailDesc.text = proto.desc;
            //detailImage.sprite = proto.sp;

            detailPanel.SetActive(true);
        }

        public void OnClickConfirm()
        {
            RoguelikeRewardSystem.instance.AddPerk(_tempRoguelikeIdentifier);
            Hide();
        }

        public void Show()
        {
            cg.alpha = 0;
            cg.DOKill();
            cg.DOFade(1, 0.35f);

            cg.interactable = true;
            cg.blocksRaycasts = true;
            GameTime.timeScale = 0;
        }

        public void Hide()
        {
            cg.DOKill();
            cg.alpha = 0;

            cg.interactable = false;
            cg.blocksRaycasts = false;

            GameTime.timeScale = 1;
        }
    }
}