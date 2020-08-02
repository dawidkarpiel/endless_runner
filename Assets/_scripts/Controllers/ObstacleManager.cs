using System;
using System.Collections.Generic;
using EndlessRunner.Obstacle;
using EndlessRunner.Stage.Segment;

namespace EndlessRunner.Controllers
{
    public class ObstacleManager
    {
        private Settings _settings;
        private ObstacleFacade.Factory _obstacleFactory;

        private Random _random = new Random();
        
        public ObstacleManager(Settings settings, ObstacleFacade.Factory obstacleFactory)
        {
            _settings = settings;
            _obstacleFactory = obstacleFactory;
        }

        public void SpawnObstacle(StageSegment stageSegment)
        {
            var randomObstacle = _settings.Obstacles[_random.Next(_settings.Obstacles.Count)];
            _obstacleFactory.Create(randomObstacle.gameObject);
        }
        
        [Serializable]
        public class Settings
        {
            public List<ObstacleFacade> Obstacles;
        }
    }
}