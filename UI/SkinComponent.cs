using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using FruitsVSJunks.Scripts.Game.Models;
using FruitsVSJunks.Scripts.Interfaces.Services;
using UniRx.Triggers;

namespace FruitsVSJunks.Scripts.UI
{
    public class SkinComponent : MonoBehaviour
    {
        public SkinData SkinData { get; private set; }

        public Button Button;

        [SerializeField] private GameObject lockedIcon;
        [SerializeField] private GameObject unlockedIcon;
        [SerializeField] private GameObject selectedFrame;
        [SerializeField] private GameObject unselectedFrame;

        private IUserService userService;

        public void Init(IUserService userService, SkinData skinData)
        {
            this.userService = userService;
            SkinData = skinData;

            lockedIcon.transform.GetChild(1).GetComponent<Image>().sprite = SkinData.LockedSprite;
            unlockedIcon.transform.GetChild(1).GetComponent<Image>().sprite = SkinData.UnlockedSprite;

            switch (skinData)
            {
                case CharacterSkinData skin:
                    ToggleSelected(userService.SelectedCharacterSkinRX.Value == skin.Id);
                    ToggleUnlocked(userService.UnlockedCharacterSkinsRX.Contains(skin.Id));
                    break;
                case EnvironmentSkinData skin:
                    ToggleSelected(userService.SelectedEnvironmentSkinRX.Value == skin.Id);
                    ToggleUnlocked(userService.UnlockedEnvironmentSkinsRX.Contains(skin.Id));
                    break;
                case TrailSkinData skin:
                    ToggleSelected(userService.SelectedTrailSkinRX.Value == skin.Id);
                    ToggleUnlocked(userService.UnlockedTrailSkinsRX.Contains(skin.Id));
                    break;
            }

            Subscribe();
        }

        private void Subscribe()
        {
            switch (SkinData)
            {
                case CharacterSkinData skin:
                    userService.UnlockedCharacterSkinsRX.ObserveCountChanged()
                        .Where(x => userService.UnlockedCharacterSkinsRX.Contains(skin.Id))
                        .Subscribe(x => ToggleUnlocked(true))
                        .AddTo(this);
                    break;
                case EnvironmentSkinData skin:
                    userService.UnlockedEnvironmentSkinsRX.ObserveCountChanged()
                        .Where(x => userService.UnlockedEnvironmentSkinsRX.Contains(skin.Id))
                        .Subscribe(x => ToggleUnlocked(true))
                        .AddTo(this);
                    break;
                case TrailSkinData skin:
                    userService.UnlockedTrailSkinsRX.ObserveCountChanged()
                        .Where(x => userService.UnlockedTrailSkinsRX.Contains(skin.Id))
                        .Subscribe(x => ToggleUnlocked(true))
                        .AddTo(this);
                    break;
            }

            Button.OnPointerClickAsObservable()
                .Subscribe(x => ToggleSelected(true))
                .AddTo(this);
        }

        public void ToggleSelected(bool selected)
        {
            selectedFrame.SetActive(selected);
            unselectedFrame.SetActive(!selected);

            transform.DOKill();

            if (selected)
            {
                transform.DOScale(1.2f, 0.3f)
                    .SetEase(Ease.OutBack);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }

        private void ToggleUnlocked(bool unlocked)
        {
            lockedIcon.SetActive(!unlocked);
            unlockedIcon.SetActive(unlocked);
        }
    }
}
