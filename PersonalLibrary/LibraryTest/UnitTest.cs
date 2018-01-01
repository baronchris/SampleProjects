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

        //[Test]
        //public void CanPopulateDatabase()
        //{
        //    List<Book> Books = DataImport.ImportCSV();
        //    BookRepoAdo repo = new BookRepoAdo();
        //    repo.PopulateDatabase(Books);
        //    Assert.IsNotEmpty(Books);
        //}

        [Test]
        public void CanGetAllBooks()
        {
            BookRepoAdo repo = new BookRepoAdo();
            List<Book> Books = repo.LoadAllBooks();
            Assert.IsNotEmpty(Books);
        }
        [Test]
        [TestCase (999, 10)]
        [TestCase (106, 5)]
        [TestCase(672, 1)]
        public void CanGetAuthorsforbook(int bookid, int expected)
        {
            BookRepoAdo repo = new BookRepoAdo();
            List<Author> authors = repo.GetAuthorsByBook(bookid);
            Assert.AreEqual(expected, authors.Count);
        }
        [Test]
        [TestCase(37,22)]
        [TestCase(169, 6)]
        [TestCase(23, 1)]
        public void CanGetBooksByAuthor(int AuthorID, int expected)
        {
            BookRepoAdo repo = new BookRepoAdo();
            List<Book> books = repo.GetBooksByAuthor(AuthorID);
            Assert.AreEqual(expected, books.Count);
        }
    }
}
