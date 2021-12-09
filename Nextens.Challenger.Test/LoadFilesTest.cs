using Nextens.Challenger.Context;
using NUnit.Framework;
using System.Linq;

namespace Nextens.Challenger.Test
{
    public class LoadFilesTest
    {
        public LoadData _loadData { get; set; }
        [SetUp]
        public void Setup()
        {
            _loadData = new LoadData();
        }

        [Test]
        public void CheckIfFilesCanBeLoaded()
        {
            Assert.IsTrue(_loadData.LoadDataset().Count() == 290);
        }
 
    }
}
