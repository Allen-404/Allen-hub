using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

namespace RoguelikeCombat
{
    public class RoguelikeRewardSystem : MonoBehaviour
    {
        public static RoguelikeRewardSystem instance;
        public RoguelikeRewardConfig config;
        public List<RoguelikeIdentifier> perks = new List<RoguelikeIdentifier>();

        private void Awake()
        {
            instance = this;
            ClearPerks();
        }

        public RoguelikeRewardPrototype GetPrototype(RoguelikeIdentifier id)
        {
            foreach (var p in config.roguelikeRewards)
            {
                if (p.id == id)
                {
                    return p;
                }
            }

            return null;
        }

        public bool HasPerk(RoguelikeIdentifier id)
        {
            return perks.IndexOf(id) >= 0;
        }

        public void ClearPerks()
        {
            perks = new List<RoguelikeIdentifier>();
        }

        public void AddPerk(RoguelikeIdentifier id)
        {
            Debug.LogWarning("add perk: " + id);
            perks.Add(id);
        }

        public void StartNewEventWithDelay(float delay = 2f)
        {
            _timePassedSinceStartNewEventWithDelay = 0;
            StartCoroutine(StartNewEventWithDelayCoroutine(delay));
        }

        private float _timePassedSinceStartNewEventWithDelay = 0;
        IEnumerator StartNewEventWithDelayCoroutine(float delay)
        {
            while (_timePassedSinceStartNewEventWithDelay < delay)
            {
                _timePassedSinceStartNewEventWithDelay += Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Max(0, 1 - _timePassedSinceStartNewEventWithDelay / delay);
                yield return null;
            }
            // yield return new WaitForSeconds(delay);
            StartNewEvent();
        }

        public void StartNewEvent()
        {
            var data = new RoguelikeRewardEventData();
            //data.title = "Choose a Upgrade!";

            int rewardCount = 3;
            var pool = GetPendingPool(rewardCount);
            if (pool.Count < rewardCount)
            {
                Debug.LogError("Not enough reward to pick!");
                return;
            }

            data.rewards = pool;
            RoguelikeRewardWindowBehaviour.instance.Setup(data);
            RoguelikeRewardWindowBehaviour.instance.Show();
            com.GameTime.timeScale = 0;
            Time.timeScale = 1;
        }

        public int GetCurrentPerkCount()
        {
            return perks.Count;
        }

        public int GetPlayerLevel()
        {
            return GetCurrentPerkCount() + 1;
        }

        List<RoguelikeIdentifier> GetPendingPool(int poolSize)
        {
            var pool = new List<RoguelikeIdentifier>();
            var candidatePool = new List<RoguelikeIdentifier>();
            var availableRewardPool = config.roguelikeRewards;
            foreach (var reward in availableRewardPool)
            {
                if (perks.IndexOf(reward.id) >= 0)
                    continue;//排除已经拿到的
                if (GetPlayerLevel() < reward.minLevel)
                    continue;//排除等级不够的
                if (reward.basePerkId != RoguelikeIdentifier.None && perks.IndexOf(reward.basePerkId) < 0)
                    continue;   //排除没有拿到前置的
                //Debug.Log("可选：" + reward.id);
                candidatePool.Add(reward.id);
            }

            for (int i = 0; i < poolSize; i++)
            {
                var randomIndex = Random.Range(0, candidatePool.Count);
                pool.Add(candidatePool[randomIndex]);
                candidatePool.RemoveAt(randomIndex);
            }

            return pool;
        }
    }
}
