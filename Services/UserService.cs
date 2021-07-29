using UniRx;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Scripting;
using System.Collections.Generic;
using FruitsVSJunks.Scripts.Character;
using FruitsVSJunks.Scripts.Interfaces.Services;
using System;
using FruitsVSJunks.Scripts.Game.Models;
using UnityEngine;

namespace FruitsVSJunks.Scripts.Services
{
    [Preserve]
    public class UserService : IUserService
    {
        // Selected/unlocked fruit skins
        public IReactiveProperty<FruitType> SelectedCharacterSkinRX { get; private set; }
        public IReactiveCollection<FruitType> UnlockedCharacterSkinsRX { get; private set; }

        // Selected/unlocked ground skins
        public IReactiveProperty<EnvironmentSkinType> SelectedEnvironmentSkinRX { get; private set; }
        public IReactiveCollection<EnvironmentSkinType> UnlockedEnvironmentSkinsRX { get; private set; }

        // Selected/unlocked trail skins
        public IReactiveProperty<TrailSkinType> SelectedTrailSkinRX { get; private set; }
        public IReactiveCollection<TrailSkinType> UnlockedTrailSkinsRX { get; private set; }

        public IReactiveProperty<int> CoinsRX { get; }
        public IReactiveProperty<int> TemporaryCoinsRX { get; }

        public IReactiveProperty<int> KeysRX { get; }
        public IReactiveProperty<int> TemporaryKeysRX { get; }

        public IReactiveProperty<int> DiamondsRX { get; }
        public IReactiveProperty<int> TemporaryDiamondsRX { get; }

        // Progress
        public ReactiveDictionary<FruitType, int> SavedFruitsRX { get; private set; }

        public UserService()
        {
            InitCharacterSkins();
            InitEnvironmentSkins();
            InitTrailSkins();

            CoinsRX = new ReactiveProperty<int>(StorageService.GetInt(PlayerPrefInts.Coins, 0));
            TemporaryCoinsRX = new ReactiveProperty<int>(0);

            KeysRX = new ReactiveProperty<int>(StorageService.GetInt(PlayerPrefInts.Keys, 0));
            TemporaryKeysRX = new ReactiveProperty<int>(0);

            DiamondsRX = new ReactiveProperty<int>(StorageService.GetInt(PlayerPrefInts.Diamonds, 0));
            TemporaryDiamondsRX = new ReactiveProperty<int>(0);

            Subscribe();

            // Check if the first fruit was unlocked by default
            if (UnlockedCharacterSkinsRX.Count == 0)
                UnlockedCharacterSkinsRX.Add(0);

            // Check if the first environment was unlocked by default
            if (UnlockedEnvironmentSkinsRX.Count == 0)
                UnlockedEnvironmentSkinsRX.Add(0);

            // Check if the first trail skin was unlocked by default
            if (UnlockedTrailSkinsRX.Count == 0)
                UnlockedTrailSkinsRX.Add(0);
        }

        public void UpdateSavedFruitsProgress(FruitType fruitType, int incrementBy)
        {
            if (SavedFruitsRX.ContainsKey(fruitType))
                SavedFruitsRX[fruitType] += incrementBy;
            else
                SavedFruitsRX[fruitType] = incrementBy;
        }

        private void InitCharacterSkins()
        {
            var unlockedSkins = JsonConvert.DeserializeObject<List<FruitType>>(StorageService.GetString(PlayerPrefStrings.UnlockedCharacterSkins));
            var selectedSkin = (FruitType) StorageService.GetInt(PlayerPrefInts.SelectedCharacterSkin, 0);
            SelectedCharacterSkinRX = new ReactiveProperty<FruitType>(selectedSkin);
            UnlockedCharacterSkinsRX = unlockedSkins != null ? new ReactiveCollection<FruitType>(unlockedSkins) : new ReactiveCollection<FruitType>();
        }

        private void InitEnvironmentSkins()
        {
            var unlockedSkins = JsonConvert.DeserializeObject<List<EnvironmentSkinType>>(StorageService.GetString(PlayerPrefStrings.UnlockedEnvironmentSkins));
            var selectedSkin = (EnvironmentSkinType) StorageService.GetInt(PlayerPrefInts.SelectedEnvironmentSkin, 0);
            SelectedEnvironmentSkinRX = new ReactiveProperty<EnvironmentSkinType>(selectedSkin);
            UnlockedEnvironmentSkinsRX = unlockedSkins != null ? new ReactiveCollection<EnvironmentSkinType>(unlockedSkins) : new ReactiveCollection<EnvironmentSkinType>();
        }

        private void InitTrailSkins()
        {
            var unlockedSkins = JsonConvert.DeserializeObject<List<TrailSkinType>>(StorageService.GetString(PlayerPrefStrings.UnlockedTrailSkins));
            var selectedSkin = (TrailSkinType) StorageService.GetInt(PlayerPrefInts.SelectedTrailSkin, 0);
            SelectedTrailSkinRX = new ReactiveProperty<TrailSkinType>(selectedSkin);
            UnlockedTrailSkinsRX = unlockedSkins != null ? new ReactiveCollection<TrailSkinType>(unlockedSkins) : new ReactiveCollection<TrailSkinType>();
        }

        /// <summary>
        /// Update the user stored player prefs when any of the data changes
        /// </summary>
        private void Subscribe()
        {
            CoinsRX
                .Skip(1)
                .Subscribe(x =>
                {
                    StorageService.SetInt(PlayerPrefInts.Coins, x);
                });

            KeysRX
                .Skip(1)
                .Subscribe(x =>
                {
                    StorageService.SetInt(PlayerPrefInts.Keys, x);
                });

            SelectedCharacterSkinRX
                .Skip(1)
                .Where(x => x < FruitType.Unknown)
                .Subscribe(x =>
                {
                    StorageService.SetInt(PlayerPrefInts.SelectedCharacterSkin, (int) x);
                });

            UnlockedCharacterSkinsRX.ObserveCountChanged()
                .Subscribe(x =>
                {
                    StorageService.SetString(PlayerPrefStrings.UnlockedCharacterSkins, JsonConvert.SerializeObject(UnlockedCharacterSkinsRX.ToList()));
                });

            SelectedEnvironmentSkinRX
                .Skip(1)
                .Subscribe(x =>
                {
                    StorageService.SetInt(PlayerPrefInts.SelectedEnvironmentSkin, (int) x);
                });

            UnlockedEnvironmentSkinsRX.ObserveCountChanged()
                .Subscribe(x =>
                {
                    StorageService.SetString(PlayerPrefStrings.UnlockedEnvironmentSkins, JsonConvert.SerializeObject(UnlockedEnvironmentSkinsRX.ToList()));
                });

            SelectedTrailSkinRX
                .Skip(1)
                .Subscribe(x =>
                {
                    StorageService.SetInt(PlayerPrefInts.SelectedTrailSkin, (int) x);
                });

            UnlockedTrailSkinsRX.ObserveCountChanged()
                .Subscribe(x =>
                {
                    StorageService.SetString(PlayerPrefStrings.UnlockedTrailSkins, JsonConvert.SerializeObject(UnlockedTrailSkinsRX.ToList()));
                });

            Observable.Merge(SavedFruitsRX.ObserveAdd().Select(x => true),
                    SavedFruitsRX.ObserveRemove().Select(x => true),
                    SavedFruitsRX.ObserveReplace().Select(x => true))
                .Throttle(TimeSpan.FromTicks(1))
                .Subscribe(x =>
                {
                    StorageService.SetString(PlayerPrefStrings.ProgressSavedFruits, JsonConvert.SerializeObject(SavedFruitsRX));
                });
        }
    }
}
