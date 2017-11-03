using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryService.Models
{
    public class RepoFactory
    {
       public static IRepository Create()
        {
            //return new DVDMockrepo();
           //return new DVDRepoADO();
           return new DVDRepoEntity();
        }
    }
}