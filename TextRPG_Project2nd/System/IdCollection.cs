using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.Character.Monster;
using TextRPG_Project2nd.Dogma;
using TextRPG_Project2nd.Item;
using TextRPG_Project2nd.Item.Cloth;
using TextRPG_Project2nd.Item.Consumables;
using TextRPG_Project2nd.Item.Weapon;

namespace TextRPG_Project2nd.System
{
    internal class IdCollection
    {
        public List<IItem> itemCollection = new List<IItem>() {new HealingPotion1(), new SoulStoneFragment(), new StrengthPotion(), 
                                                               new WondererCloth(), new BluntRod(), new LeaderArmor(), new ShortSword() };
        public List<IDogma> dogmaCollection = new List<IDogma>() { new DogmaNatalana(), new DogmaErzebeta() };
    }
}
