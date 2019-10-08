using System;
using System.Collections.Generic;
using System.Text;

namespace ArmyPlannerData
{
    public class Specialty
    {
       public string SpecialismName { get; set; }
       public int Level { get; set; }
       public  int LevelCost { get; set; }
       public  List<SpecialistAbility> Abilities { get; set; }
    }

    public class SpecialistAbility
    {
        string AbilityName { get; set; }
        bool isTactic { get; set; }
        string AbilityDescription { get; set; }
        int cmdPointCost { get; set; }
    }
}
