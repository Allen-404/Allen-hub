using UnityEngine;
using System;

namespace RoguelikeCombat
{
    public enum RoguelikeIdentifier
    {
        None = 0,
        HE=1,
        AP=2,
        InvArmor=3,
        DamageUp1 = 4,
        DamageUp2=5,
        ExtraCannon1=6,
        ExtraCannon2=7,
        RunningFire1=8,
        RunningFire2=9,
        HeavyArmor=10,
        LightArmor=11,
        EnergyShield=12,
        Mine=13,
        UAV1 = 14,
        UAV2 = 15,
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