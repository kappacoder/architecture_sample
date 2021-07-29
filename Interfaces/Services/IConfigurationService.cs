using FruitsVSJunks.Scripts.Game.Models;

namespace FruitsVSJunks.Scripts.Interfaces.Services
{
    public interface IConfigurationService
    {
        CharacterSkinData[] CharacterSkins { get; }
        EnvironmentSkinData[] EnvironmentSkins { get; }
        TrailSkinData[] TrailSkins { get; }
    }
}
