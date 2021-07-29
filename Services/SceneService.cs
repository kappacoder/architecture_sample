using System;
using UniRx;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.SceneManagement;
using FruitsVSJunks.Scripts.Interfaces.Services;

namespace FruitsVSJunks.Scripts.Services
{
    public enum SceneName
    {
        Splash,
        Menu
    }

    [Preserve]
    public class SceneService : ISceneService
    {
        public IReactiveProperty<SceneName> CurrentSceneRX { get; }

        private AsyncOperation loadSceneOperation;
        private SceneName sceneToLoad;

        public SceneService()
        {
            CurrentSceneRX = new ReactiveProperty<SceneName>();
        }

        public void GoToScene(SceneName scene)
        {
            sceneToLoad = scene;
            loadSceneOperation = SceneManager.LoadSceneAsync(scene.ToString());

            loadSceneOperation.completed += OnSceneLoadCompleted;
        }

        public void LoadSceneAdditively(string scene, Action<AsyncOperation> onCompleted = null)
        {
            var operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            if (onCompleted != null)
                operation.completed += onCompleted;
        }

        public void UnloadScene(string scene)
        {
            SceneManager.UnloadSceneAsync(scene);
        }

        private void OnSceneLoadCompleted(AsyncOperation operation)
        {
            CurrentSceneRX.Value = sceneToLoad;
        }
    }
}
