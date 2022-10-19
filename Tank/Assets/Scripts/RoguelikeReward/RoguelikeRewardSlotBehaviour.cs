using UnityEngine;
using UnityEngine.UI;

namespace RoguelikeCombat
{
    public class RoguelikeRewardSlotBehaviour : MonoBehaviour
    {
        public Image icon;
        RoguelikeRewardPrototype _proto;

        public void Show(RoguelikeIdentifier r)
        {

            _proto = RoguelikeRewardSystem.instance.GetPrototype(r);
            icon.sprite = _proto.sp;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnClick()
        {
            RoguelikeRewardWindowBehaviour.instance.ShowDetail(_proto);
        }
    }
}