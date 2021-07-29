using Adic;
using Adic.Container;
using CMF;
using FruitsVSJunks.Scripts.Game.Controllers;
using FruitsVSJunks.Scripts.Game.Models;
using FruitsVSJunks.Scripts.Interfaces.Game.Controllers;
using FruitsVSJunks.Scripts.Interfaces.Services;
using FruitsVSJunks.Scripts.Services;
using FruitsVSJunks.Scripts.Utils;
using RollyVortex.Scripts.Interfaces.Services;
using UnityEngine;

namespace FruitsVSJunks.Scripts.Game
{
    /// <summary>
    /// Entry point for the game. This is the dependency injection root.
    /// Here all services are initialized and bound to their interfaces.
    /// </summary>
    public class GameContainer : ContextRoot
    {
#region Initialization assets and prefabs
        [Header("General")]
        [SerializeField] private GameObject characterPrefab;

        [Header("Skins")]
        [SerializeField] private CharacterSkinData[] characterSkins;
        [SerializeField] private EnvironmentSkinData[] environmentSkins;
        [SerializeField] private TrailSkinData[] trailSkins;
#endregion

        public override void SetupContainers()
        {
            IInjectionContainer container = new InjectionContainer()
                .RegisterExtension<UnityBindingContainerExtension>()
                .RegisterExtension<EventCallerContainerExtension>()
                .RegisterExtension<AdicSharedContainerExtension>();

            // You can easily replace different implementations of the interfaces here.
            // For example: if you want to test a different script for GameService just bound the interface to it:
            // .Bind<IGameService>().To(new GameServiceTest(characterPrefab))
            container
                .Bind<IConfigurationService>().To(new ConfigurationService(characterSkins, environmentSkins, trailSkins))
                .Bind<IUserService>().ToSingleton<UserService>()
                .Bind<AudioService>().ToGameObject("AudioService")
                .Bind<IGameService>().To(new GameService(characterPrefab))
                .Bind<UIService>().ToGameObject("UIService");

            AddContainer(container);
        }

        public override void Init()
        {
            Application.targetFrameRate = 60;
        }
    }
}
