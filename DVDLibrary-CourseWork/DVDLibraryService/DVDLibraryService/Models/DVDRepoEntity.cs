using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.SqlClient;
using System.Data.Entity.Core.Objects;

namespace DVDLibraryService.Models
{
    
    public class DVDRepoEntity : IRepository
    {
        public void AddDVD(DVD ToAdd)
        {
            DVDLibraryEntities Libris = new DVDLibraryEntities();
            Libris.DVDs.Add(ToAdd);
            Libris.SaveChanges();
            Note ToAddNote = new Note
            {
                
                DVDID = (Libris.DVDs.Max(d => d.DVDID)),
                Notes = ToAdd.Note
            };
            Libris.Notes.Add(ToAddNote);
            Libris.SaveChanges();
        }

        public void DeleteDvD(int DVDID) //WORKS
        {
            DVDLibraryEntities Libris = new DVDLibraryEntities();

            var dvd = Libris.DVDs.Where(d => d.DVDID == DVDID).FirstOrDefault();
            if (dvd !=null)
            {
                Libris.Notes.Remove(Libris.Notes.Where(d => d.DVDID == DVDID).FirstOrDefault());
                Libris.DVDs.Remove(Libris.DVDs.Where(d => d.DVDID == DVDID).FirstOrDefault());
                Libris.SaveChanges();
            }
        }

        public void EditDVD(DVD ToEdit)
        {
            DVDLibraryEntities Libris = new DVDLibraryEntities();

            DVD dvd = new DVD();
            dvd.DVDID = ToEdit.DVDID;
            dvd.Title = ToEdit.Title;
            dvd.Director = ToEdit.Director;
            dvd.RatingID = ToEdit.RatingID;
            dvd.RatingName = ToEdit.RatingName;
            dvd.realeaseYear = ToEdit.realeaseYear;
            Note noteedit = Libris.Notes.Where(n => n.DVDID == ToEdit.DVDID).FirstOrDefault();
            noteedit.Notes = ToEdit.Note;
            Libris.Entry(dvd).State = System.Data.Entity.EntityState.Modified;
            Libris.Entry(noteedit).State = System.Data.Entity.EntityState.Modified;
            Libris.SaveChanges();
        }

        public IEnumerable<DVDDetailView> FindByDirector(string director)
        {
            List<DVDDetailView> FinalResult = new List<DVDDetailView>();
            DVDLibraryEntities Libris = new DVDLibraryEntities();
            var results = from d in Libris.DVDs
                          where d.Director.Contains(director)
                          select d;
                        
           foreach(DVD r in results)
            {
               
                    DVDDetailView current = new DVDDetailView();
                    current.DVDID = r.DVDID;
                    current.Title = r.Title;
                current.Director = r.Director;
                    current.RatingID = r.RatingID;
                    current.RatingName = r.Rating.RatingName;
                    current.realeaseYear = r.realeaseYear;
                current.realeaseYear = r.realeaseYear;
                if (Libris.Notes.Where(n => n.DVDID == r.DVDID).FirstOrDefault().Notes != null)
                {
                    current.Note = Libris.Notes.Where(n => n.DVDID == r.DVDID).FirstOrDefault().Notes;
                }
                FinalResult.Add(current);
                
            }
            return FinalResult;

           
            
        }

        public IEnumerable<DVDDetailView> FindByTitle(string title)
        {
            List<DVDDetailView> FinalResult = new List<DVDDetailView>();
            DVDLibraryEntities Libris = new DVDLibraryEntities();
            var results = from d in Libris.DVDs
                          where d.Title.Contains(title)
                          select d;

            foreach (DVD r in results)
            {

                DVDDetailView current = new DVDDetailView();
                current.DVDID = r.DVDID;
                current.Title = r.Title;
                current.Director = r.Director;
                current.RatingID = r.RatingID;
                current.RatingName = r.Rating.RatingName;
                current.realeaseYear = r.realeaseYear;
                current.realeaseYear = r.realeaseYear;
                if (Libris.Notes.Where(n => n.DVDID == r.DVDID).FirstOrDefault().Notes != null)
                {
                    current.Note = Libris.Notes.Where(n => n.DVDID == r.DVDID).FirstOrDefault().Notes;
                }
                FinalResult.Add(current);

            }
            return FinalResult;
        }

        public IEnumerable<DVDDetailView> FindbyYear(int year)
        {
            List<DVDDetailView> FinalResult = new List<DVDDetailView>();
            DVDLibraryEntities Libris = new DVDLibraryEntities();
            var results = from d in Libris.DVDs
                          where d.realeaseYear ==year
                          select d;

            foreach (DVD r in results)
            {

                DVDDetailView current = new DVDDetailView();
                current.DVDID = r.DVDID;
                current.Title = r.Title;
                current.Director = r.Director;
                current.RatingID = r.RatingID;
                current.RatingName = r.Rating.RatingName;
                current.realeaseYear = r.realeaseYear;
                if (!string.IsNullOrEmpty(Libris.Notes.Where(n => n.DVDID == r.DVDID).FirstOrDefault().Notes))
                {
                    current.Note = Libris.Notes.Where(n => n.DVDID == r.DVDID).FirstOrDefault().Notes;
                }
                FinalResult.Add(current);

            }
            return FinalResult;
        }

        public IEnumerable<DVDListView> GetAll() //WORKS
        {
            DVDLibraryEntities Libris = new DVDLibraryEntities();
            var results = from DVD in Libris.DVDs
                          select new DVDListView
                          {
                              DVDID=DVD.DVDID,
                              Director=DVD.Director,
                              RatingName=DVD.Rating.RatingName,
                              RatingID=DVD.RatingID,
                              realeaseYear=DVD.realeaseYear,
                              Title=DVD.Title
                          };
            return results.ToList();

        }

        public DVDDetailView GetByID(int ID)  //works
        {
            DVDLibraryEntities Libris = new DVDLibraryEntities();
            DVD result = Libris.DVDs.Where(d => d.DVDID == ID).FirstOrDefault();

            if (Libris.Notes.Where(n => n.DVDID == ID).FirstOrDefault().Notes == null)
            {
                result.Note = "no note";
            }
            else
            {
                result.Note = Libris.Notes.Where(n => n.DVDID == ID).FirstOrDefault().Notes;
            }
            DVDDetailView Selected = new DVDDetailView
            {
                DVDID = result.DVDID,
                Director = result.Director,
                RatingName = result.Rating.RatingName,
                RatingID=result.RatingID,
                realeaseYear = result.realeaseYear,
                Title = result.Title,
                Note=result.Note
            };
            return Selected;

        }
    }
}