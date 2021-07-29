using FruitsVSJunks.Scripts.Character;
using FruitsVSJunks.Scripts.Game.Models;
using FruitsVSJunks.Scripts.Services;
using FruitsVSJunks.Scripts.UI;
using UniRx;

namespace FruitsVSJunks.Scripts.Interfaces.Services
{
    public interface IUserService
    {
        IReactiveProperty<FruitType> SelectedCharacterSkinRX { get; }
        IReactiveCollection<FruitType> UnlockedCharacterSkinsRX { get; }

        IReactiveProperty<EnvironmentSkinType> SelectedEnvironmentSkinRX { get; }
        IReactiveCollection<EnvironmentSkinType> UnlockedEnvironmentSkinsRX { get; }

        IReactiveProperty<TrailSkinType> SelectedTrailSkinRX { get; }
        IReactiveCollection<TrailSkinType> UnlockedTrailSkinsRX { get; }

        IReactiveProperty<int> CoinsRX { get; }
        IReactiveProperty<int> TemporaryCoinsRX { get; }

        IReactiveProperty<int> KeysRX { get; }
        IReactiveProperty<int> TemporaryKeysRX { get; }
        
        IReactiveProperty<int> DiamondsRX { get; }
        IReactiveProperty<int> TemporaryDiamondsRX { get; }

        ReactiveDictionary<FruitType, int> SavedFruitsRX { get; }

        void UpdateSavedFruitsProgress(FruitType fruitType, int incrementBy);
    }
}
