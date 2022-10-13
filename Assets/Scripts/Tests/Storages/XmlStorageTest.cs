using NUnit.Framework;
using Shooter.SaveSystem;

namespace Shooter.Test.Storages
{
    public sealed class XmlStorageTest
    {
        private const string Path = "xmlstorage tst";

        [Test]
        public void SavesCorrectly()
        {
            IStorage storage = new XmlStorage();
            var data = new Data
            {
                IsNotNull = true
            };
            
            storage.Save(Path, data);
            Assert.That(storage.Load<Data>(Path).IsNotNull);
        }
        
    }

    public struct Data
    {
        public bool IsNotNull;
        
    }
}