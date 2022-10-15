using Shooter.Model;
using Shooter.SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class ScoreRoot : SerializedMonoBehaviour, IScoreRoot
    {
        [SerializeField] private IView<int> _scoreBestRecordView;
        [SerializeField] private IView<int> _scoreView;
        [SerializeField, Min(1)] private int _scoreAddAmount = 40;

        private IScore _score;

        public IScore Score()
        {
            _score ??= Compose();
            return _score;
        }

        private IScore Compose()
        {
            IScoreBestRecord scoreBestRecord = new ScoreBestRecord(_scoreBestRecordView, new StorageWithNameSaveObject<ScoreBestRecord, int>());
            _score = new Score(_scoreView, scoreBestRecord);
            new ScoreAfterRandomTimeAdder(_score, _scoreAddAmount).TryAddLoop().Forget();
            return _score;
        }
    }
}