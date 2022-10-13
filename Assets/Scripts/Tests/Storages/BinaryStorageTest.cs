using NUnit.Framework;
using Shooter.SaveSystem;

namespace Shooter.Test.Storages
{
    public sealed class BinaryStorageTest
    {
        private const string Path = "54";

        [Test]
        public void SavesCorrectly()
        {
            IStorage storage = new BinaryStorage();
            const int count = 44;
            storage.Save(Path, count);
            Assert.That(storage.Load<int>(Path) == count);
        }
    }
}
