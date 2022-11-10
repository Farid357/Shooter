using System;
using NUnit.Framework;
using Shooter.SaveSystem;

namespace Shooter.Test.Storages
{
    public sealed class JsonStorageTest
    {
        private const string Path = "mono test";

        [Test]
        public void SavesCorrectly()
        {
            IStorage storage = new JsonStorage();
            storage.Save(Path, new Data(46));
            Assert.That(storage.Load<Data>(Path).Count == 46);
        }
        
        [Serializable]
        private class Data
        {
            public int Count;

            public Data(int count)
            {
                Count = count;
            }
        }
    }
}
