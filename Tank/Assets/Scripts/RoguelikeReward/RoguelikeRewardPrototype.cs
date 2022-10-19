using UnityEngine;
using System;

namespace RoguelikeCombat
{
    public enum RoguelikeIdentifier
    {
        None = 0,
        Cat_Rune_Obtain_2,
        Cat_Rune_Obtain_4,
        Cat_Rune_Obtain_6,
        Cat_Rune_PassiveHpMax_5,
        Cat_Rune_PassiveHpMax_7,
        Cat_Rune_PassiveHpMax_10,
        Crab_Rune_Obtain_2,
        Crab_Rune_Obtain_4,
        Crab_Rune_Obtain_6,
        Crab_Rune_PassiveAtkMax_5,
        Crab_Rune_PassiveAtkMax_7,
        Crab_Rune_PassiveAtkMax_10,

        PlaceHolder_1 = 101,
        PlaceHolder_2 = 102,
        PlaceHolder_3 = 103,
        PlaceHolder_4 = 104,
        PlaceHolder_5 = 105,
        PlaceHolder_6 = 106,
    }

    [CreateAssetMenu]
    [Serializable]
    public class RoguelikeRewardPrototype : ScriptableObject
    {
        public RoguelikeIdentifier id;
        public RoguelikeIdentifier basePerkId;
        public int minLevel;

        public string title;
        [Multiline]
        public string desc;
        public Sprite sp;
    }
}