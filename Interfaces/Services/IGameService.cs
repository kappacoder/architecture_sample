using FruitsVSJunks.Scripts.Character;
using FruitsVSJunks.Scripts.Scriptables;
using FruitsVSJunks.Scripts.Services;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace FruitsVSJunks.Scripts.Interfaces.Services
{
    public interface IGameService
    {
        IReactiveProperty<MiniGameStates> MiniGameStateRX { get; set; }

        public IReactiveProperty<GameStates> StateRX { get; }

        IReactiveProperty<float> CurrentLevelProgressRX { get; }

        IReactiveProperty<int> CurrentScoreRX { get; }

        IReactiveCollection<PlayableCharacterComponent> CharactersRX { get; }

        IReactiveProperty<bool> IsInvincibleRX { get; }

        MiniGameData CurrentMiniGameData { get; set; }

        BoxCollider CurrentFinishGoal { get; set; }

        float CurrentMinigameMultiplier { get; set; }

        List<FruitType> CurrentLevelTotalFruits { get; set; }

        Vector3 MainCharacterStartPosition { get; }

        PlayableCharacterComponent GetMainCharacter();

        Vector3 GetNextCartLandingPosition();

        void KillCharacter(PlayableCharacterComponent character);

        void Reset();

        void GoToNextLevel();

        void Dispose();

        void TriggerInvincibility();
    }
}
