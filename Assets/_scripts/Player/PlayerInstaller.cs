using Zenject;

namespace EndlessRunner.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public PlayerMoveController.Settings PlayerMoveController;
        public override void InstallBindings()
        {
            Container.BindInstance(PlayerMoveController);
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle().NonLazy();
        }
    }
}