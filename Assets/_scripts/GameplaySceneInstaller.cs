using EndlessRunner.Obstacle;
using EndlessRunner.Controllers;
using EndlessRunner.Controllers.Input;
using EndlessRunner.Stage;
using EndlessRunner.Player;
using EndlessRunner.Signals;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public PlayerFacade PlayerFacade;
    public StageManager.Settings StageManagerSettings;
    public ObstacleManager.Settings ObstacleManagerSettings;
    public UserInterfaceController.Settings UserInterfaceControllerSettings;

    public override void InstallBindings()
    {
        BindControllers();
        BindStage();
        BindPlayer();
        DeclareSignals();
    }

    private void BindControllers()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        Container.BindInterfacesAndSelfTo<StandaloneInputController>().AsSingle().NonLazy();
#else
			Container.Bind<ITickable>().To<MobileInputController>().AsSingle().NonLazy();
#endif

        Container.Bind<SaveController>().AsSingle().NonLazy();

        Container.BindInstance(UserInterfaceControllerSettings);
        Container.BindInterfacesAndSelfTo<UserInterfaceController>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<GameFlowManager>().AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle().NonLazy();
    }

    private void BindStage()
    {
        Container.BindInstance(StageManagerSettings);
        Container.BindFactory<GameObject, StageFacade, StageFacade.Factory>().FromFactory<PrefabFactory<StageFacade>>();
        Container.BindInterfacesAndSelfTo<StageManager>().AsSingle().NonLazy();

        Container.BindInstance(ObstacleManagerSettings);
        Container.BindFactory<GameObject, ObstacleFacade, ObstacleFacade.Factory>()
            .FromFactory<PrefabFactory<ObstacleFacade>>();
        Container.Bind<ObstacleManager>().AsSingle();
    }

    private void BindPlayer()
    {
        Container.Bind<PlayerFacade>().FromComponentInNewPrefab(PlayerFacade).AsSingle().NonLazy();
    }

    private void DeclareSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameOverSignal>();
        Container.DeclareSignal<GameStartSignal>();
        Container.DeclareSignal<ScoreIncreaseSignal>();
        Container.DeclareSignal<BestScoreSignal>();
    }
}