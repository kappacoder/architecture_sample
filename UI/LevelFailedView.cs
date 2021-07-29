using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace FruitsVSJunks.Scripts.UI
{
    public class LevelFailedView : BaseView
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private TMP_Text coinsWonText;

        protected override void Subscribe()
        {
            restartButton.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    gameService.Reset();
                })
                .AddTo(this);
        }

        public override void Show()
        {
            int coinsWon = userService.TemporaryCoinsRX.Value;
            userService.CoinsRX.Value += coinsWon;
            userService.TemporaryCoinsRX.Value = 0;
            userService.TemporaryKeysRX.Value = 0;

            coinsWonText.text = $"+{coinsWon.ToString()} <sprite name=\"coin\">";

            base.Show();
        }
    }
}
