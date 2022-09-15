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

        public IScore ComposeScore()
        {
            IScoreBestRecord scoreBestRecord = new ScoreBestRecord(_scoreBestRecordView,
                new StorageWithNameSaveObject<ScoreBestRecord, int>());
            var score = new Score(_scoreView, scoreBestRecord);
            new ScoreAfterRandomTimeAdder(score, _scoreAddAmount).TryAddLoop();
            return score;
        }

    }
}