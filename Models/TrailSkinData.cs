using UnityEngine;

namespace FruitsVSJunks.Scripts.Game.Models
{
    public enum TrailSkinType 
    {
        None,
        Fire,
        Lightning,
        Ice,
        Rainbow
    }

    [CreateAssetMenu(fileName = "TrailSkinData", menuName = "RunawayFruits/Skins/New Trail Skin Data", order = 2)]
    public class TrailSkinData : SkinData
    {
        public TrailSkinType Id;
    }
}