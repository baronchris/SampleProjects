using DVDLibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class MockRepoTest
    {
       [Test]
       public void CanAddDVD()
        {
            IRepository repo = RepoFactory.Create();
            DVD ToADD = new DVD { Title = "TestMovie", Director = "Test", RatingID = 1, Note = "nononon", realeaseYear = 1977 };
            repo.AddDVD(ToADD);
            List<DVDListView> DVDs = repo.GetAll().ToList();
            Assert.AreEqual(3, DVDs.Count());
            Assert.AreEqual(3, DVDs[2].DVDID);
            Assert.AreEqual(DVDs[2].Title, "TestMovie");
        }
        [Test]
        public void CanDeleteDVD()
        {
            IRepository repo = RepoFactory.Create();
            repo.DeleteDvD(1);
            List<DVDListView> DVDs = repo.GetAll().ToList();
            Assert.AreEqual(1, DVDs.Count());

        }
    }
}
