using FruitsVSJunks.Scripts.Scriptables;
using UnityEngine;

namespace FruitsVSJunks.Scripts.Game.Models 
{
    public enum LevelDifficulty
    {
        Easy,
        Normal,
        Challenge,
        Bonus
    }

    [CreateAssetMenu(fileName = "LevelData", menuName = "RunawayFruits/New Level Data", order = 2)]
    public class LevelData : ScriptableObject
    {
        public LevelDifficulty Difficulty;

        public MiniGameData MiniGameData;
        
        public float TotalDistance;
        
        public Vector3 LevelStartPosition = Vector3.zero;
    }
}
