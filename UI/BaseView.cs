using Adic;
using UnityEngine;
using FruitsVSJunks.Scripts.Interfaces.Services;
using FruitsVSJunks.Scripts.Services;
using UnityEngine.UI;

namespace FruitsVSJunks.Scripts.UI
{
    public class BaseView : MonoBehaviour
    {
        [Inject]
        protected UIService uiService;
        [Inject]
        protected IUserService userService;
        [Inject]
        protected IGameService gameService;
        [Inject]
        protected AudioService audioService;

        protected Canvas canvas;

        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();

            // Adjust the UI scale depending whether the device is a phone or a tablet
            CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

            if (canvasScaler != null)
                canvasScaler.referenceResolution = Utils.Utils.DeviceIsTablet() ?
                    new Vector2(1536, 2048) : new Vector2(1080, 1920);
        }

        protected void Start()
        {
            this.Inject();
        }

        [Inject]
        protected void PostConstruct()
        {
            Init();
            Subscribe();
        }

        protected virtual void Init()
        {
        }

        protected virtual void Subscribe()
        {
        }

        public virtual void Hide()
        {
            canvas.enabled = false;
        }

        public virtual void Show()
        {
            canvas.enabled = true;
        }
    }
}
