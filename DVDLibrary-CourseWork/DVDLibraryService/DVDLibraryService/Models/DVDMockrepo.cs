using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryService.Models
{
    public class DVDMockrepo : IRepository
    {
        private List<DVD> _DVDs = new List<DVD> {
        new DVD
        {
            DVDID=1,
            Title = "A movie",
            Director = "SomeGuy",
            RatingName="R",
            realeaseYear=1995,
            Note="This is a note.  It is my note.  It is my note that I wrote"
        },
        new DVD
        {
            DVDID = 2,
            Title = "Another movie",
            Director = "Some other Guy",
            RatingName = "PG",
            realeaseYear = 1998,
            Note = "This is a note.  It is my note.  It is my note that I wrote"
        }, };
        private List<DVDListView> _DVDsView = new List<DVDListView> {
        new DVDListView
        {
            DVDID=1,
            Title = "A movie",
            Director = "SomeGuy",
            RatingName="R",
            realeaseYear=1995
            
        },
        new DVDListView
        {
            DVDID = 2,
            Title = "Another movie",
            Director = "Some other Guy",
            RatingName = "PG",
            realeaseYear = 1998
        }, };

        public void AddDVD(DVD ToAdd)
        {
            if (_DVDs.Any())
            {
                ToAdd.DVDID = _DVDs.Max(d =>d.DVDID) + 1;
            }
            else
            {
                ToAdd.DVDID =1;
            }
            DVD AddedDVD = new DVD();

            
                int rating = (int)ToAdd.RatingID;
                AddedDVD.RatingName = GetRatingName(rating);

            
            AddedDVD.DVDID = ToAdd.DVDID;
            AddedDVD.Title = ToAdd.Title;
            AddedDVD.Director = ToAdd.Director;
            AddedDVD.realeaseYear = ToAdd.realeaseYear;
            AddedDVD.Note = ToAdd.Note;
            

            _DVDs.Add(AddedDVD);
        }

        public void DeleteDvD(int ID)
        {
            _DVDs.RemoveAll(d => d.DVDID == ID);
        }

        public void EditDVD(DVD ToEdit)
        {
            _DVDs.RemoveAll(d => d.DVDID == ToEdit.DVDID);
            _DVDs.Add(ToEdit);
        }

        public IEnumerable<DVDDetailView> FindByDirector(string director)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DVDDetailView> FindByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DVDDetailView> FindbyYear(int year)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DVDListView> GetAll()
        {
            return _DVDsView;
        }

        public DVDDetailView GetByID(int id)
        {
            DVD result = _DVDs.Where(d => d.DVDID == id).FirstOrDefault();
            DVDDetailView Selected = new DVDDetailView
            {
                DVDID = result.DVDID,
                Director = result.Director,
                RatingName = result.Rating.RatingName,
                RatingID = result.RatingID,
                realeaseYear = result.realeaseYear,
                Title = result.Title,
                Note = result.Note
            };
            return Selected;
        }
        public string GetRatingName(int rating)
        {
            string ratingName = "";
            switch (rating)
            {
                case 1:
                    ratingName = "G";
                    return ratingName;
                case 2:
                    ratingName = "PG";
                    return ratingName;
                case 3:
                    ratingName = "PG-13";
                    return ratingName;
                case 4:
                    ratingName = "R";
                    return ratingName;
                case 5:
                    ratingName = "NC-17";
                    return ratingName;
                default:
                    ratingName = "Unrated";
                    return ratingName;
            }
        }
    }
}