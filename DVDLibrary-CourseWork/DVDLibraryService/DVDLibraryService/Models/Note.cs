//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DVDLibraryService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Note
    {
        public int DVDID { get; set; }
        public int NoteID { get; set; }
        public string Notes { get; set; }
    
        public virtual DVD DVD { get; set; }
    }
}
