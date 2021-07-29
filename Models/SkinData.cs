using FruitsVSJunks.Scripts.UI;
using UnityEngine;

namespace FruitsVSJunks.Scripts.Game.Models 
{
    public enum SkinType
    {
        Character,
        Environment,
        Trail,
        Accessory
    }

    public class SkinData : ScriptableObject
    {
        public SkinType SkinType;
        public Currency Currency;
        public int Price;
        public Sprite LockedSprite;
        public Sprite UnlockedSprite;
    }
}
