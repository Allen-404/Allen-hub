using UnityEngine;
using System.Collections.Generic;
using System;
using com;
using RoguelikeCombat;

public class CoolDownSystem : MonoBehaviour
{
    public static CoolDownSystem instance;

    Dictionary<RoguelikeIdentifier, float> _perkCdTimestamp = new Dictionary<RoguelikeIdentifier, float>();

    private void Awake()
    {
        instance = this;
    }

    public void TryTrigger(RoguelikeIdentifier id, Action cb, float interval)
    {
        bool has = RoguelikeRewardSystem.instance.HasPerk(id);
        if (has)
        {
            if (_perkCdTimestamp.ContainsKey(id))
            {
                if (GameTime.time > _perkCdTimestamp[id] + interval)
                {
                    _perkCdTimestamp[id] = GameTime.time;
                    cb.Invoke();
                }
            }
            else
            {
                _perkCdTimestamp.Add(id, GameTime.time);
                cb.Invoke();
            }
        }
    }
}