using Adic;
using TMPro;
using UniRx;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using FruitsVSJunks.Scripts.Services;
using FruitsVSJunks.Scripts.Scriptables;
using FruitsVSJunks.Scripts.Interfaces.Services;

namespace FruitsVSJunks.Scripts.UI
{
    public class GameView : BaseView
    {
        [SerializeField] private Image progressSlider;
        [SerializeField] private TMP_Text level;
        [SerializeField] private GameObject bottomPanel;

        [Inject]
        private ILevelService levelService;

        protected override void Subscribe()
        {
            gameService.CurrentLevelProgressRX
                .Subscribe(progress =>
                {
                    progressSlider.fillAmount = progress;
                })
                .AddTo(this);

            gameService.StateRX
                .Where(x => x == GameStates.AllCharactersInCart)
                .Subscribe(x =>
                {
                    bottomPanel.SetActive(false);
                })
                .AddTo(this);
        }

        public override void Show()
        {
            UpdateUI();

            base.Show();
        }

        private void UpdateUI()
        {
            level.text = levelService.CurrentLevelRX.Value.ToString();
        }
    }
}
