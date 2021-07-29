using Adic;
using UniRx;
using System;
using UnityEngine.Scripting;
using FruitsVSJunks.Scripts.Interfaces.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace FruitsVSJunks.Scripts.Services
{
    [Preserve]
    public class LevelService : ILevelService
    {
        public IReactiveProperty<int> CurrentLevelRX { get; }

        [Inject]
        private ISceneService sceneService;

        private int currentlyLoadedScene = -1;

        public LevelService()
        {
            CurrentLevelRX = new ReactiveProperty<int>(StorageService.GetInt(PlayerPrefInts.CurrentLevel, 1));

            Subscribe();
        }

        public void ReloadCurrentLevel(Action onCompleted = null)
        {
            if (currentlyLoadedScene != -1)
            {
                sceneService.UnloadScene(currentlyLoadedScene.ToString());
                sceneService.LoadSceneAdditively(currentlyLoadedScene.ToString(), _ => onCompleted?.Invoke());
            }
            else
            {
                currentlyLoadedScene = CurrentLevelRX.Value;
                sceneService.LoadSceneAdditively(CurrentLevelRX.Value.ToString(), _ => onCompleted?.Invoke());
            }
        }

        public void LoadNextLevel(Action onCompleted = null)
        {
            if (currentlyLoadedScene != -1)
                sceneService.UnloadScene(currentlyLoadedScene.ToString());

            CurrentLevelRX.Value += 1;

            currentlyLoadedScene = CurrentLevelRX.Value;
            sceneService.LoadSceneAdditively(CurrentLevelRX.Value.ToString(), _ => onCompleted?.Invoke());
        }

        private void Subscribe()
        {
            CurrentLevelRX
                .Subscribe(x =>
                {
                    StorageService.SetInt(PlayerPrefInts.CurrentLevel, x);
                });
        }
    }
}
