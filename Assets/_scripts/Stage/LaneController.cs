using System;

namespace EndlessRunner.Stage
{
    public class LaneController
    {
        private Settings _settings;

        public LaneController(Settings settings)
        {
            _settings = settings;
        }

        public int LanesCount => _settings.laneOffsets.Length;

        public float PlayerSpawnPosition => _settings.laneOffsets[_settings.playerSpawnLine];

        public int PlayerSpawnLine => _settings.playerSpawnLine;

        public float GetLaneOffset(int laneNumber)
        {
            return _settings.laneOffsets[laneNumber];
        }

        [Serializable]
        public class Settings
        {
            public int playerSpawnLine;
            public float[] laneOffsets;
        }
    }
}