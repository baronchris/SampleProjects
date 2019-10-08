using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
        public Specialty Specilism { get; set; }
        public int ModelPoints { get; set; }
        public int TotalPoints { get; set; }
        //constructor - if fireteam null get speciality
        public KillTeamCharacter(string name, string FactionName, int modelid, string specialty )
        {

        }

        private StatLine GetLine(int ModelTypeID)
        {
            StatLine stats = new StatLine();

            return stats;
        }
        private int GetTotalCost()
        {
            int cost;
            cost = this.Specilism.LevelCost + this.ModelPoints;
            int GearCost = gear.Sum(g => g.Cost);
            cost += GearCost;
            return cost;

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
