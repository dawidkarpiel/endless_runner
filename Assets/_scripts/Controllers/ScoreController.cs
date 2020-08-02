using EndlessRunner.Signals;
using Zenject;

namespace EndlessRunner.Controllers
{
    public class ScoreController : IInitializable
    {
        private SignalBus _signalBus;
        private SaveController _saveController;

        public float CurrentScore { get; private set; }

        public float BestScore
        {
            get => _saveController.BestScore;
            private set => _saveController.BestScore = value;
        }

        public ScoreController(SignalBus signalBus, SaveController saveController)
        {
            _signalBus = signalBus;
            _saveController = saveController;
        }

        public void Initialize()
        {
            SubscribeToSignals();
        }

        private void SubscribeToSignals()
        {
            _signalBus.Subscribe<ScoreIncreaseSignal>(OnScoreChange);
            _signalBus.Subscribe<GameStartSignal>(OnGameStart);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        private void OnScoreChange(ScoreIncreaseSignal signal)
        {
            CurrentScore += signal.ScoreChange;
        }

        private void OnGameStart(GameStartSignal signal)
        {
            CurrentScore = 0;
        }

        private void OnGameOver(GameOverSignal signal)
        {
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
                _saveController.BestScore = BestScore;
                _signalBus.Fire(new BestScoreSignal(BestScore));
            }
        }
    }
}