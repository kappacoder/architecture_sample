using System;
using UniRx;

namespace FruitsVSJunks.Scripts.Interfaces.Services
{
    public interface ILevelService
    {
        IReactiveProperty<int> CurrentLevelRX { get; }

        void ReloadCurrentLevel(Action onCompleted = null);

        void LoadNextLevel(Action onCompleted = null);
    }
}
