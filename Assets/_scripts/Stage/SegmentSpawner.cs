using System;
using System.Collections.Generic;
using System.Linq;
using EndlessRunner.Controllers;
using EndlessRunner.Stage.Segment;
using EndlessRunner.Player;
using Zenject;

namespace EndlessRunner.Stage
{
    public class SegmentSpawner : ITickable
    {
        private List<StageSegment> _activeSegments = new List<StageSegment>();
        private int SpawnedSegmentsCount = 0;
        private StageSegment _lastSpawnedSegment;
        private bool _isStageActive;

        private StageSegment.Pool _segmentsPool;
        private PlayerFacade _playerFacade;
        private Settings _settings;
        private ObstacleManager _obstacleManager;

        public SegmentSpawner(StageSegment.Pool segmentsPool, PlayerFacade playerFacade, Settings settings,
            ObstacleManager obstacleManager)
        {
            _segmentsPool = segmentsPool;
            _playerFacade = playerFacade;
            _settings = settings;
            _obstacleManager = obstacleManager;
        }

        public StageSegment GetLastSpawnedSegment()
        {
            return _lastSpawnedSegment;
        }

        public bool HasStageEnded()
        {
            return SpawnedSegmentsCount >= _settings.stageSegmentsCount;
        }

        public void StartStage()
        {
            _isStageActive = true;

            for (int i = -_settings.segmentsBehindPlayer; i <= _settings.activeSegments; i++)
            {
                SpawnSegment();
            }
        }

        public void StopStage()
        {
            _isStageActive = true;
        }

        public void Tick()
        {
            if (!_isStageActive)
            {
                return;
            }

            var segmentsClone = new List<StageSegment>(_activeSegments);
            foreach (var stageSegment in segmentsClone)
            {
                if (ShouldBeDisposed(stageSegment))
                    DisposeSegment(stageSegment);
            }

            for (int i = _activeSegments.Count; i <= _settings.activeSegments; i++)
            {
                SpawnSegment();
                SpawnObstacle();
            }
        }

        private bool ShouldBeDisposed(StageSegment segment)
        {
            if (segment.Position >= _playerFacade.PlayerPosition)
                return false;

            return _playerFacade.PlayerPosition - segment.Position >
                   _settings.segmentWidth * _settings.segmentsBehindPlayer;
        }

        private void DisposeSegment(StageSegment segment)
        {
            _segmentsPool.Despawn(segment);
            _activeSegments.Remove(segment);
        }

        private void SpawnSegment()
        {
            var position = 0f;
            if(_lastSpawnedSegment != null)
                position = _lastSpawnedSegment.transform.position.z + _settings.segmentWidth;
            
            var segment = _segmentsPool.Spawn(position);
            _activeSegments.Add(segment);
            _lastSpawnedSegment = segment;
            SpawnedSegmentsCount++;
        }

        private void SpawnObstacle()
        {
            if (SpawnedSegmentsCount % _settings.obstaclesInterval == 0)
            {
                _obstacleManager.SpawnObstacle(_lastSpawnedSegment);
            }
        }


        [Serializable]
        public class Settings
        {
            public int activeSegments;
            public float segmentWidth;
            public int stageSegmentsCount;
            public int segmentsBehindPlayer;
            public int obstaclesInterval;
        }
    }
}