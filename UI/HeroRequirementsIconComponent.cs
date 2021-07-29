using Adic;
using UniRx;
using MPUIKIT;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using FruitsVSJunks.Scripts.Character;
using FruitsVSJunks.Scripts.Interfaces.Services;
using System.Linq;

namespace FruitsVSJunks.Scripts.UI
{
    /// <summary>
    /// This UI component indicates how many fruits the player has collected
    /// versus how many he has to collect in this level
    /// </summary>
    public class HeroRequirementsIconComponent : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text text;
        [SerializeField] private GameObject incompleteMark;

        [Inject]
        private IGameService gameService;

        private FruitType fruitType;
        private int requirement;
        private int actual;
        private bool showIncompleteMark;

        private void Start()
        {
            this.Inject();
        }

        public void Init(FruitType fruitType, int requirement, int actual,
            bool subscribeForChange, bool showIncompleteMark = false)
        {
            this.fruitType = fruitType;
            this.requirement = requirement;
            this.actual = actual;
            this.showIncompleteMark = showIncompleteMark;

            iconImage.sprite = sprites[(int) fruitType];

            Refresh();

            if (subscribeForChange)
                Subscribe();
        }

        private void Subscribe()
        {
            gameService.CharactersRX.ObserveCountChanged()
                .Subscribe(x =>
                {
                    actual = gameService.CharactersRX.Count(c => c.FruitType == fruitType);
                    Refresh();
                })
                .AddTo(this);
        }

        public void Refresh()
        {
            text.text = $"{Math.Min(actual, requirement)}/{requirement}";

            if (showIncompleteMark && actual < requirement)
                incompleteMark.SetActive(true);
            else
                incompleteMark.SetActive(false);
        }
    }
}
