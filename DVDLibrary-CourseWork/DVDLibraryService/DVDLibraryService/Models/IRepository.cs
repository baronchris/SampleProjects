using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDLibraryService.Models
{
    public interface IRepository
    {
       IEnumerable<DVDListView> GetAll();
        DVDDetailView GetByID(int ID);
        void AddDVD(DVD ToAdd);
        void EditDVD(DVD ToEdit);
        void DeleteDvD(int DVDID);
        IEnumerable<DVDDetailView> FindByTitle(string title);
        IEnumerable<DVDDetailView> FindByDirector(string director);
        IEnumerable<DVDDetailView> FindbyYear(int year); 
    }
}
