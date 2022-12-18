using UnityEngine;
using System;

namespace RoguelikeCombat
{
    public enum RoguelikeIdentifier
    {
        None = 0,//ok
        HE=1,//ok
        AP =2,//ok
        InvArmor =3,
        DamageUp1 = 4,//ok
        DamageUp2 =5,//ok
        ExtraCannon1 =6,//ok
        ExtraCannon2 =7,//ok
        RunningFire1 =8,//ok
        RunningFire2 =9,//ok
        HeavyArmor =10,//ok
        LightArmor =11,//ok
        EnergyShield =12,//ok
        Mine =13,//ok
        UAV1 = 14,//unused
        UAV2 = 15,//unused
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