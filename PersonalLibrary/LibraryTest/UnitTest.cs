using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PersonalLibrary.Data.Models;
using PersonalLibrary.Data;

namespace LibraryTest
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void CanLoadBooks()
        {

            List<Book> Books = DataImport.ImportCSV();
            Assert.IsNotEmpty(Books);
        }

        [Test]
        public void CanPopulateDatabase()
        {
            List<Book> Books = DataImport.ImportCSV();
            BookRepoAdo repo = new BookRepoAdo();
            repo.PopulateDatabase(Books);
            Assert.IsNotEmpty(Books);
        }

    }
}
