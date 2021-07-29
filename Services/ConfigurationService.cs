using Adic;
using FruitsVSJunks.Scripts.Game.Models;
using FruitsVSJunks.Scripts.Interfaces.Services;
using UnityEngine.Scripting;

namespace FruitsVSJunks.Scripts.Services
{
    [Preserve]
    public class ConfigurationService : IConfigurationService
    {
        public CharacterSkinData[] CharacterSkins { get; }
        public EnvironmentSkinData[] EnvironmentSkins { get; }
        public TrailSkinData[] TrailSkins { get; }

        public ConfigurationService(CharacterSkinData[] characterSkins, EnvironmentSkinData[] environmentSkins,
            TrailSkinData[] trailSkins)
        {
            CharacterSkins = characterSkins;
            EnvironmentSkins = environmentSkins;
            TrailSkins = trailSkins;
        }
    }
}
