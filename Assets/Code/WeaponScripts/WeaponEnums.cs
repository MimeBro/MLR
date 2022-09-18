
using System;

namespace Code.WeaponScripts
{
    public enum HitTimings
    {
        Miss,
        Ok,
        Good,
        Perfect
    }

    [Flags]
    public enum WeaponType
    {
        Sword = 1, 
        GreatSword = 1 << 1, 
        Axe = 1 << 2, 
        Hammer = 1 << 3, 
        Spear = 1 << 4, 
        Bow = 1 << 5, 
        Staff = 1 << 6, 
        Book = 1 << 7
    }

    public enum WeaponRarity
    {
        Common, 
        Rare, 
        Epic, 
        Legendary, 
        Mythic
    }

}