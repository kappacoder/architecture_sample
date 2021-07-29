using Adic;
using UnityEngine;
using FruitsVSJunks.Scripts.Interfaces.Services;
using FruitsVSJunks.Scripts.Services;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace FruitsVSJunks.Scripts.UI
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private Button shopButton;
        [SerializeField] private TMP_Text currentLevel;

        [Inject] private ILevelService levelService;

        protected override void Subscribe()
        {
            levelService.CurrentLevelRX
                .Subscribe(x =>
                {
                    currentLevel.text = "Level " + x;
                })
                .AddTo(this);

            shopButton.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    uiService.OpenShop();
                })
                .AddTo(this);
        }
    }
}
