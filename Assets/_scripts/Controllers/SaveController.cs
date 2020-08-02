using UnityEngine;
using Zenject;

namespace EndlessRunner.Controllers
{
    public class SaveController
    {
        private const string BestScoreKey = "BestScore";

        public float BestScore
        {
            get { return PlayerPrefs.GetFloat(BestScoreKey, 0); }
            set { PlayerPrefs.SetFloat(BestScoreKey, value); }
        }
    }
}