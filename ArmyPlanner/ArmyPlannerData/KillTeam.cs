using System;
using System.Collections.Generic;
using System.Text;

namespace ArmyPlannerData
{
    public class KillTeam
    {
        public string Player { get; set; }
        public string Faction { get; set; }
        public string TeamName { get; set; }
        public string Mission { get; set; }
        public string Quirk { get; set; }
        public List<KillTeamCharacter> Team { get; set; }
    }
}
