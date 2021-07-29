using Adic;
using UniRx;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using FruitsVSJunks.Scripts.Interfaces.Services;
using FruitsVSJunks.Scripts.UI;

namespace FruitsVSJunks.Scripts.Services
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private Image shade;
        [Header("Views")]
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private GameView gameView;
        [SerializeField] private FinishLineMinigameView finishLineMinigameView;
        [SerializeField] private LevelFailedView levelFailedView;
        [SerializeField] private LevelCompletedView levelCompletedView;
        [SerializeField] private ShopView shopView;

        [Inject]
        private IGameService gameService;

        private void Awake()
        {
            // Adjust the UI scale depending whether the device is a phone or a tablet
            CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

            canvasScaler.referenceResolution = Utils.Utils.DeviceIsTablet() ?
                new Vector2(1536, 2048) : new Vector2(1080, 1920);
        }

        private void Start()
        {
            this.Inject();
        }

        [Inject]
        private void PostConstruct()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            gameService.StateRX
                .Where(state => state == GameStates.None)
                .Skip(1)
                .Subscribe(x =>
                {
                    gameView.Hide();
                    levelFailedView.Hide();
                    levelCompletedView.Hide();
                    mainMenuView.Hide();

                    shade.color = Color.white;
                })
                .AddTo(this);

            gameService.StateRX
                .Where(state => state == GameStates.LevelLoaded)
                .Subscribe(x =>
                {
                    gameView.Hide();
                    levelFailedView.Hide();
                    levelCompletedView.Hide();
                    mainMenuView.Show();

                    shade.DOFade(0f, 0.3f);
                })
                .AddTo(this);

            gameService.StateRX
                .Where(state => state == GameStates.LevelStarted)
                .Subscribe(x =>
                {
                    // Hide main menu
                    mainMenuView.Hide();
                    gameView.Show();
                })
                .AddTo(this);

            gameService.StateRX
                .Where(state => state == GameStates.AllCharactersInCart)
                .Subscribe(x =>
                {
                    finishLineMinigameView.Show();
                })
                .AddTo(this);

            gameService.StateRX
                .Where(state => state == GameStates.LevelFailed)
                .Subscribe(x =>
                {
                    finishLineMinigameView.Hide();
                    gameView.Hide();
                    levelFailedView.Show();
                })
                .AddTo(this);

            gameService.StateRX
                .Where(state => state == GameStates.LevelCompleted)
                .Subscribe(x =>
                {
                    finishLineMinigameView.Hide();
                    gameView.Hide();
                    levelCompletedView.Show();
                })
                .AddTo(this);
        }

        public void OpenShop()
        {
            mainMenuView.Hide();
            shopView.Show();
        }

        public void HideShop()
        {
            mainMenuView.Show();
            shopView.Hide();
        }
    }
}
