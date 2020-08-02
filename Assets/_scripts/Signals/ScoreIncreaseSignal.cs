namespace EndlessRunner.Signals
{
    public class ScoreIncreaseSignal
    {
        public ScoreIncreaseSignal(float scoreChange)
        {
            ScoreChange = scoreChange;
        }

        public float ScoreChange
        {
            get;
            private set;
        }
    }
}