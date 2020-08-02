using System;
using EndlessRunner.Signals;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace EndlessRunner.Controllers
{
    public class UserInterfaceController : IInitializable, ITickable
    {
        private Settings _settings;
        private ScoreController _scoreController;
        private SignalBus _signalBus;
        private GameFlowManager _gameFlowManager;

        public UserInterfaceController(Settings settings, ScoreController scoreController, SignalBus signalBus,
            GameFlowManager gameFlowManager)
        {
            _settings = settings;
            _scoreController = scoreController;
            _signalBus = signalBus;
            _gameFlowManager = gameFlowManager;
        }


        public void Initialize()
        {
            SubscribeToSignals();
            _settings.GameStartButton.onClick.AddListener(onGameStartButtonClicked);
            _settings.BestScoreText.text = RoundUpFloat(_scoreController.BestScore).ToString();
        }

        private void onGameStartButtonClicked()
        {
            _gameFlowManager.StartGame();
            _settings.GameStartButton.gameObject.SetActive(false);
        }

        private void SubscribeToSignals()
        {
            _signalBus.Subscribe<BestScoreSignal>(onNewBestScore);
            _signalBus.Subscribe<GameOverSignal>(onGameOver);
        }

        public void Tick()
        {
            _settings.ScoreText.text = RoundUpFloat(_scoreController.CurrentScore).ToString();
        }

        private void onNewBestScore(BestScoreSignal signal)
        {
            _settings.BestScoreText.text = RoundUpFloat(signal.NewBestScore).ToString();
        }

        private void onGameOver()
        {
            _settings.GameStartButton.gameObject.SetActive(true);
        }

        private int RoundUpFloat(float floatToRound)
        {
            return (int) Math.Round(floatToRound);
        }

        [Serializable]
        public class Settings
        {
            public TMP_Text ScoreText;
            public TMP_Text BestScoreText;
            public Button GameStartButton;
        }
    }
}