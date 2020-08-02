using System;
using System.Collections.Generic;
using EndlessRunner.Stage;
using Zenject;

namespace EndlessRunner.Controllers
{
    public class StageManager : IInitializable, ITickable
    {
        private Settings _settings;
        private StageFacade.Factory _stageFactory;
        private StageFacade _currentStageFacade;

        private Queue<StageFacade> StageQueue = new Queue<StageFacade>();

        public StageManager(Settings settings, StageFacade.Factory stageFactory)
        {
            _settings = settings;
            _stageFactory = stageFactory;
        }

        public StageFacade CurrentStageFacade => _currentStageFacade;

        public void Initialize()
        {
            StageQueue.Enqueue(_settings.RandomStageFacade);
            _settings.PredefinedStages.ForEach(stage => StageQueue.Enqueue(stage));
            
            _currentStageFacade = _stageFactory.Create(GetNextStage().gameObject);
            _currentStageFacade.StartStage();
        }

        public void Tick()
        {
            if (_currentStageFacade.HasStageEnded)
            {
                var nextStage = GetNextStage();
                if (nextStage == null)
                    return;
                
                _currentStageFacade.StopStage();

                _currentStageFacade = nextStage;
                _currentStageFacade.StartStage();
            }
        }

        private StageFacade GetNextStage()
        {
            if (StageQueue.Count == 0)
                return null;

            return StageQueue.Dequeue();
        }

        [Serializable]
        public class Settings
        {
            public List<StageFacade> PredefinedStages;
            public StageFacade RandomStageFacade;
        }
    }
}