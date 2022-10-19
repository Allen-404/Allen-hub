using UnityEngine;
using System.Collections.Generic;
using System;

namespace RoguelikeCombat
{
    [CreateAssetMenu]
    [Serializable]
    public class RoguelikeRewardConfig : ScriptableObject
    {
        public List<RoguelikeRewardPrototype> roguelikeRewards;
    }

    [Serializable]
    public class RoguelikeRewardEventData
    {
        public List<RoguelikeIdentifier> rewards = new List<RoguelikeIdentifier>();
        public string title;
    }
}