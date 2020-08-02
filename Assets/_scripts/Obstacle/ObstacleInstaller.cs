using Zenject;

namespace EndlessRunner.Obstacle
{
    public class ObstacleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObstacleFacade>().FromComponentOn(this.gameObject).AsSingle();
        }
    }
}
