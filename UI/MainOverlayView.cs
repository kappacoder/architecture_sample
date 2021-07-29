using Adic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using FruitsVSJunks.Scripts.Services;
using FruitsVSJunks.Scripts.Interfaces.Services;

namespace FruitsVSJunks.Scripts.UI
{
    public class MainOverlayView : BaseView
    {
        [SerializeField] private TMP_Text coins;
        [SerializeField] private Sprite keySprite;
        [SerializeField] private Sprite keyEmptySprite;
        [SerializeField] private Image[] keyImages;

        [Inject]
        private IUserService userService;

        protected override void Subscribe()
        {
            Observable.Merge(userService.CoinsRX,
                    userService.TemporaryCoinsRX)
                .Subscribe(x =>
                {
                    if (gameService.StateRX.Value == GameStates.LevelStarted)
                        coins.text = $"{(userService.CoinsRX.Value + userService.TemporaryCoinsRX.Value).ToString()} <sprite name=\"coin\">";
                    else
                        coins.text = $"{userService.CoinsRX.Value.ToString()} <sprite name=\"coin\">";
                })
                .AddTo(this);

            Observable.Merge(userService.KeysRX,
                    userService.TemporaryKeysRX)
                .Subscribe(x =>
                {
                    if (gameService.StateRX.Value == GameStates.LevelStarted)
                    {
                        for (int i = 0; i < keyImages.Length; i++)
                            keyImages[i].sprite = userService.KeysRX.Value + userService.TemporaryKeysRX.Value > i ? keySprite : keyEmptySprite;
                    }
                    else
                    {
                        for (int i = 0; i < keyImages.Length; i++)
                            keyImages[i].sprite = userService.KeysRX.Value > i ? keySprite : keyEmptySprite;
                    }
                })
                .AddTo(this);
        }
    }
}
