using System;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Shooter.GameLogic;
using Shooter.Model;

namespace Shooter.Test
{
    [TestFixture]
    public sealed class TimerTest
    {
        [Test]
        public void EndsCorrectly()
        {
            ITimer timer = new Timer(new DummySecondsView(), 0.2f);
            UniTask.Create(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
                Assert.That(timer.IsEnded);
            });
        }
    }
}