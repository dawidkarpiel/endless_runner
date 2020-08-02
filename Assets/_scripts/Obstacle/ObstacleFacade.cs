using EndlessRunner.Controllers;
using EndlessRunner.Player;
using EndlessRunner.Signals;
using EndlessRunner.Stage.Utils;
using UnityEngine;
using Zenject;

namespace EndlessRunner.Obstacle
{
    public class ObstacleFacade : MonoBehaviour, IColliderTriggerEnter
    {
        private StageManager _stageManager;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(StageManager stageManager, SignalBus signalBus)
        {
            _stageManager = stageManager;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            var lastSegment = _stageManager.CurrentStageFacade.GetLastSpawnedSegment();
            lastSegment.AddObstacle(this);
            this.transform.position = lastSegment.transform.position;
        }

        public void TriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerCollider>() != null)
                _signalBus.Fire<GameOverSignal>();
        }

        public class Factory : PlaceholderFactory<GameObject, ObstacleFacade>
        {
        }
    }
}