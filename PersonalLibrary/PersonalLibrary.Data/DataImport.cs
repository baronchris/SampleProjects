using PersonalLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonalLibrary.Data
{
    public static class DataImport
    {
       public static Book ImportBook(string current)
        {
            Book NewBook = new Book();
            List<string> fields = Regex.Split(current, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)").ToList();   //current.Split(',').ToList();
            NewBook.Title = fields[5];
            NewBook.ISBN = fields[8];
            string authors = fields[6];
            string authorsTrim = authors.Trim('"');
            NewBook.AuthorsToAdd = authorsTrim.Split(',').ToList();
            return NewBook; 
        }

        public static List<Book> ImportCSV()
        {
            List<Book> Books = new List<Book>();
            string path = "C:\\GoogleBookscsv.txt";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            Book current = ImportBook(line);
                            Books.Add(current);
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Cannot access CSV");
            }
            return Books;
        }
    }
}
