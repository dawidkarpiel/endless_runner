namespace EndlessRunner.Signals
{
    public class BestScoreSignal
    {
        public BestScoreSignal(float newBestScore)
        {
            NewBestScore = newBestScore;
        }

        public float NewBestScore
        {
            get;
            private set;
        }
    }
}