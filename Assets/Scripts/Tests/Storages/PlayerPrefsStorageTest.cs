using NUnit.Framework;
using Shooter.SaveSystem;

namespace Shooter.Test.Storages
{
    public sealed class PlayerPrefsStorageTest
    {
        private const string Path = "prefs";

        [Test]
        public void SavesCorrectly()
        {
            IStorage storage = new PlayerPrefsStorage();
            const int count = 54;
            storage.Save(Path, count);
            Assert.That(storage.Load<int>(Path) == count);
        }
    }
}
