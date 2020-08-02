using EndlessRunner.Signals;
using UnityEngine;
using Zenject;

namespace EndlessRunner.Controllers
{
    public class GameFlowManager : IInitializable
    {
        private SignalBus _signalBus;

        public GameFlowManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            SubscribeToSignals();
        }

        private void SubscribeToSignals()
        {
            _signalBus.Subscribe<GameOverSignal>(GameOver);
        }

        public void StartGame()
        {
            _signalBus.Fire<GameStartSignal>();
            Debug.Log("Game Start");
        }

        private void GameOver(GameOverSignal gameOverSignal)
        {
            Debug.Log("GameOver");
        }
    }
}