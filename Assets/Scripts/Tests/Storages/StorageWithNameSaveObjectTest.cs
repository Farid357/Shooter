using NUnit.Framework;
using Shooter.SaveSystem;

namespace Shooter.Test.Storages
{
    public sealed class StorageWithNameSaveObjectTest
    {
        [Test]
        public void SaveCorrectly()
        {
            var storage = new StorageWithNameSaveObject<StorageWithNameSaveObjectTest, int>();
            const int count = 44;
            storage.Save(count);
            Assert.That(storage.Load() == count);
        }
    }
}
