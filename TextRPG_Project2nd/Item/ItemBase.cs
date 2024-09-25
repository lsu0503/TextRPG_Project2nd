using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;

namespace TextRPG_Project2nd.Item
{
    public interface IStackable
    {
        int Amount { get; set; }
        int MaxAmount { get; }
    }

    public interface ICloth
    {
        int[] Attribute { get; }
        int[] DefStatus { get; }
    }

    public interface IWeapon
    {
        int[] TypeAtk { get; }

        IAttack Attack { get; }

        ISkill Skill { get; }
    }

    public interface IItem
    {
        int ItemType { get; }
        int ItemID { get; }
        string Name { get; }
        string[] DetailItem { get; }

        int PriceBuy { get; }
        int PriceSell { get; }
    }
}
