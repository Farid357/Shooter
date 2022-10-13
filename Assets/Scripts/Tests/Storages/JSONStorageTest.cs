using NUnit.Framework;
using Shooter.SaveSystem;

namespace Shooter.Test.Storages
{
    public sealed class JsonStorageTest
    {
        private const string Path = "mono";

        [Test]
        public void SavesCorrectly()
        {
            IStorage storage = new JsonStorage();
            const int count = 45;
            storage.Save(Path, count);
            Assert.That(storage.Load<int>(Path) == count);
        }
    }
}
