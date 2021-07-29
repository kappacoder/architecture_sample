using UnityEngine;

namespace FruitsVSJunks.Scripts.Game.Models
{
    public enum EnvironmentSkinType
    {
        Default,
        Dark
    }

    [CreateAssetMenu(fileName = "EnvironmentSkinData", menuName = "RunawayFruits/Skins/New Environment Skin Data", order = 2)]
    public class EnvironmentSkinData : SkinData
    {
        public EnvironmentSkinType Id;
        public int LevelRequirement;
    }
}