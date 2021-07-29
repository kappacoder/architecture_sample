using FruitsVSJunks.Scripts.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using Adic;
using FruitsVSJunks.Scripts.Interfaces.Services;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using TMPro;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace FruitsVSJunks.Scripts.UI
{
    public class LevelCompletedView : BaseView
    {
        [SerializeField] private Button nextButton;
        [SerializeField] private TMP_Text coinsWonText;
        [SerializeField] private Transform heroRequirementsWrapper;
        [SerializeField] private HeroRequirementsIconComponent heroRequirementIconPrefab;

        [Inject] private ILevelService levelService;

        protected override void Subscribe()
        {
            nextButton.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    gameService.GoToNextLevel();
                })
                .AddTo(this);
        }

        public override void Show()
        {
            UpdateSavedFruits();
            UpdateCoins();
            UpdateTreasureKeys();

#if !UNITY_EDITOR
            // Send a game analytics event
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level_" + levelService.CurrentLevelRX.Value);
#endif

            base.Show();

            audioService.PlayCheeringSound();
        }

        private void UpdateSavedFruits()
        {
            for (int i = heroRequirementsWrapper.childCount - 1; i >= 0; i--)
                Destroy(heroRequirementsWrapper.GetChild(i).gameObject);

            // Create a dictionary of the fruit types in the level
            // The int represents total fruits in the level
            Dictionary<FruitType, int> fruitsInLevel = new Dictionary<FruitType, int>();

            foreach (var fruit in gameService.CurrentLevelTotalFruits)
            {
                if (fruitsInLevel.ContainsKey(fruit))
                    fruitsInLevel[fruit]++;
                else
                    fruitsInLevel[fruit] = 1;
            }

            foreach (var kvp in fruitsInLevel)
            {
                FruitType fruitType = kvp.Key;
                int totalFruitsInLevel = kvp.Value;

                int savedFruits = gameService.CharactersRX.Count(x => x.FruitType == fruitType);

                HeroRequirementsIconComponent heroRequirementIcon = Instantiate(heroRequirementIconPrefab, heroRequirementsWrapper);
                heroRequirementIcon.Init(gameService, fruitType, totalFruitsInLevel, savedFruits, false);

                // Update user progress
                if (savedFruits > 0)
                    userService.UpdateSavedFruitsProgress(fruitType, savedFruits);
            }
        }

        private void UpdateCoins()
        {
            int originalCoinsWon = userService.TemporaryCoinsRX.Value;
            int multipliedCoins = (int) Mathf.Floor(originalCoinsWon * gameService.CurrentMinigameMultiplier);
            userService.CoinsRX.Value += multipliedCoins;
            userService.TemporaryCoinsRX.Value = 0;

            coinsWonText.text = $"{originalCoinsWon} <sprite name=\"coin\"> x {gameService.CurrentMinigameMultiplier} = {multipliedCoins.ToString()} <sprite name=\"coin\">";
        }

        private void UpdateTreasureKeys()
        {
            int originalKeysWon = userService.TemporaryKeysRX.Value;
            userService.KeysRX.Value += originalKeysWon;
            userService.TemporaryKeysRX.Value = 0;
        }
    }
}
