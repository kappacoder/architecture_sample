using System;
using UnityEngine;
using FruitsVSJunks.Scripts.Character;

namespace FruitsVSJunks.Scripts.Scriptables
{
    public enum ZoneType
    {
        Red,
        Orange,
        Yellow,
        Green
    }

    [CreateAssetMenu(fileName = "MinigameData", menuName = "RunawayFruits/New Minigame Data", order = 1)]
    public class MiniGameData : ScriptableObject
    {
        public MiniGameZoneData[] Zones;
    }

    [Serializable]
    public class MiniGameZoneData
    {
        public ZoneType ZoneType;
        public int[] Sections;
        public float ScoreMultiplier;
        public bool Unlockable;
        public FruitType FruitToUnlock;
        public int NeededFruitsCount;
    }
}
