using System;
using System.Collections.Generic;
using System.Text;

namespace ArmyPlannerData
{
    public class Wargear
    {
        public string ItemName { get; set; }
        public int Range { get; set; }
        public int Cost { get; set; }
        public string sDamage { get; set; }
        public string sWeaponType { get; set; }
        public int ShotNumber { get; set; }
        public string GearAbility { get; set; }

    }
    public enum Damage { one, two, three, d3, d6};
    public enum WeaponType { Assault, RapidFire, Heavy, Pistol, Grenade}
}
