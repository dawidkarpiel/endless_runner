using System.Collections.Generic;
using EndlessRunner.Obstacle;
using UnityEngine;
using Zenject;

namespace EndlessRunner.Stage.Segment
{
    public class StageSegment : MonoBehaviour
    {
        private List<ObstacleFacade> _obstacles = new List<ObstacleFacade>();

        public float Position => this.transform.position.z;

        private void Reinitialize(float spawnPosition)
        {
            this.gameObject.transform.position = Vector3.forward * spawnPosition;
        }

        public void AddObstacle(ObstacleFacade obstacle)
        {
            _obstacles.Add(obstacle);
        }

        public void DeleteObstacles()
        {
            for (int i = 0; i < _obstacles.Count; i++)
            {
                Destroy(_obstacles[i].gameObject);
            }

            _obstacles.Clear();
        }

        public class Pool : MonoMemoryPool<float, StageSegment>
        {
            protected override void Reinitialize(float spawnPosition, StageSegment segment)
            {
                segment.Reinitialize(spawnPosition);
            }

            protected override void OnDespawned(StageSegment segment)
            {
                segment.DeleteObstacles();
                base.OnDespawned(segment);
            }

            protected override void OnDestroyed(StageSegment segment)
            {
                segment.DeleteObstacles();
                base.OnDestroyed(segment);
            }
        }
    }
}