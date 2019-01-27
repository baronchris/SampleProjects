using System;
using System.Collections.Generic;
using System.Text;

namespace ArmyPlannerData
{
    public enum FactionName
    {
        Aeldari,
        Drukhari,
        Harlequin,
        Adeptus_Astartes,
        Heretic_Astartes,
        Tau,
        Astra_militarium,
        Adeptus_Mechanicus,
        Genestealer_Cult,
        Tyranid,
        Deathwatch,
        Grey_Knights,
        Thousand_Sons,
        Death_Guard,
        Necron,
        Orks
    }
    public class Faction
    {
        public string FactionName { get; set; }
        public string Race { get; set; }

        public FactionDetails GetFactionDetails(string factionname)
        {
            FactionDetails fd = new FactionDetails();
            //stuff
            return fd;
        } 
            

    }
    public class FactionDetails
    {
        public string SubfactionType1 { get; set; }
        public string SubfactionType2 { get; set; }
    }
}
