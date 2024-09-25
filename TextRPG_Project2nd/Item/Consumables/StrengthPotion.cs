using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.Item;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Item.Consumables
{
    public class StrengthPotion : IItem, IStackable, IAction
    {
        // IItemBase 구성 요소
        int itemType = 0;
        int itemID = 002;
        string name = "강주약";
        string[] detailItem= new string[] { "힘이 강해지는 약.",
                                                "원리는 알 수 없다.",
                                                "원료도 알 수 없다.",
                                                "원체 알 수가 없다."};

        int priceBuy = 500;
        int priceSell = 100;

        public int ItemType { get { return itemType; } }
        public int ItemID { get { return itemID; } }
        public string Name { get { return name; } }
        public string[] DetailItem { get { return detailItem; } }

        public int PriceBuy { get { return priceBuy; } }
        public int PriceSell { get { return priceSell; } }

        // IUsable 구성 요소
        int amount = 0;
        int maxAmount = 10;

        public int Amount { get { return amount; } set { amount = value; } }
        public int MaxAmount { get { return maxAmount; } }

        // IAction 구성요소
        int actionType = 3;
        string[] detailAction = new string[] {"사용 시 짧은 시간 공격력이 크게 상승한다."};
        int power;

        List<EffectAtk> effectAtkList = new List<EffectAtk>();
        List<IStatusEffect> effectList = new List<IStatusEffect>() { new AttackBuff(1, 60) };

        public int ActionType { get { return actionType; } }
        public string[] DetailAction { get { return detailAction; } }
        public int Power { get { return power; } }

        public List<EffectAtk> EffectAtkList { get { return effectAtkList; } }
        public List<IStatusEffect> EffectList { get { return effectList; } }

        public bool AttemptAction(ICharacter user)
        {
            return true;
        }

        public ResultBlock UseAction(ICharacter user, ICharacter target)
        {
            return new ResultBlock(0, 0, effectList, null, 0);
        }
    }
}
