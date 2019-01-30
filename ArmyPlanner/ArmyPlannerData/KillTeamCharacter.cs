using System;
using System.Collections.Generic;
using System.Text;

namespace ArmyPlannerData
{
    public class KillTeamCharacter
    {
        public string Name { get; set; }
        public Faction faction { get; set; }
        public StatLine stats { get; set; }
        public List<Wargear> gear { get; set; }
        public bool FireTeam { get; set; }
        public bool Convalescent { get; set; }
        public int ModelTypeID { get; set; }

        //constructor - if fireteam null get speciality
        public KillTeamCharacter(string name, string FactionName)
        {

        }

        private StatLine GetLine(int ModelTypeID)
        {
            StatLine stats = new StatLine();

            return stats;
        }
    }
    

    public class StatLine
    {
        public int ModelID { get; set; }
        public int Movement { get; set; }
        public int WeaponSkill { get; set; }
        public int BallisticSkill { get; set; }
        public int Strength { get; set; }
        public int Toughness { get; set; }
        public int Attacks { get; set; }
        public int Leadership { get; set; }
        public int ArmourSave { get; set; }
        public int? InvulnerableSave { get; set; }
    }
}
