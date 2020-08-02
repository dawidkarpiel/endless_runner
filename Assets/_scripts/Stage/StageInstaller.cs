using EndlessRunner.Stage.Segment;
using Zenject;

namespace EndlessRunner.Stage
{
    public class StageInstaller : MonoInstaller
    {
        public StageSegment SegmentPrefab;
        public SegmentSpawner.Settings SegmentSpawnerSettings;
        public LaneController.Settings LaneControllerSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(SegmentSpawnerSettings);
            Container.BindInstance(LaneControllerSettings);

            Container.Bind<LaneController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SegmentSpawner>().AsSingle().NonLazy();
            
            Container.BindMemoryPool<StageSegment, StageSegment.Pool>()
                .WithInitialSize(SegmentSpawnerSettings.activeSegments)
                .FromComponentInNewPrefab(SegmentPrefab)
                .UnderTransformGroup("SegmentsPool");
        }
    }
}