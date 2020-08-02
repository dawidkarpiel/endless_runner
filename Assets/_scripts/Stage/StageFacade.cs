using EndlessRunner.Stage.Segment;
using UnityEngine;
using Zenject;

namespace EndlessRunner.Stage
{
    public class StageFacade : MonoBehaviour
    {
        private LaneController _laneController;
        private SegmentSpawner _segmentSpawner;

        [Inject]
        public void Construct(LaneController laneController, SegmentSpawner segmentSpawner)
        {
            _laneController = laneController;
            _segmentSpawner = segmentSpawner;
        }

        public int LaneCount
        {
            get { return _laneController.LanesCount; }
        }

        public bool HasStageEnded
        {
            get { return _segmentSpawner.HasStageEnded(); }
        }

        public void StartStage()
        {
            _segmentSpawner.StartStage();
        }

        public void StopStage()
        {
            _segmentSpawner.StopStage();
        }

        public float GetLaneOffset(int laneNumber)
        {
            return _laneController.GetLaneOffset(laneNumber);
        }

        public float GetPlayerSpawnPosition()
        {
            return _laneController.PlayerSpawnPosition;
        }

        public StageSegment GetLastSpawnedSegment()
        {
            return _segmentSpawner.GetLastSpawnedSegment();
        }

        public int GetPlayerSpawnLine()
        {
            return _laneController.PlayerSpawnLine;
        }

        public class Factory : PlaceholderFactory<GameObject, StageFacade>
        {
        }
    }
}