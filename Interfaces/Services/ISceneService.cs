using System;
using FruitsVSJunks.Scripts.Services;
using UniRx;
using UnityEngine;

namespace FruitsVSJunks.Scripts.Interfaces.Services
{
    public interface ISceneService
    {
        IReactiveProperty<SceneName> CurrentSceneRX { get; }

        void GoToScene(SceneName scene);

        void LoadSceneAdditively(string scene, Action<AsyncOperation> onCompleted = null);

        void UnloadScene(string scene);
    }
}
