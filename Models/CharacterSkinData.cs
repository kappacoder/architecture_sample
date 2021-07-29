using FruitsVSJunks.Scripts.Character;
using UnityEngine;

namespace FruitsVSJunks.Scripts.Game.Models
{
    [CreateAssetMenu(fileName = "CharacterSkinData", menuName = "RunawayFruits/Skins/New Character Skin Data", order = 1)]
    public class CharacterSkinData : SkinData
    {
        public FruitType Id;
        public SerializableDictionary<FruitType, int> UnlockRequirements;
    }
}