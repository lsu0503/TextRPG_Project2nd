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
    public class HealingPotion1 : IItem, IStackable, IAction
    {
        // IItemBase 구성 요소
        int itemType = 0;
        int itemID = 001;
        string name = "회복약";
        string[] detailItem = new string[] { "가장 기초적인 형태의 회복약.",
                                                "원료는 달맞이 풀이라는 듯 하다.",
                                                "... 달맞이 풀은 푸른데 왜 이 약은 초록색인가.",
                                                "세기의 미스터리 그 하나."};

        int priceBuy = 50;
        int priceSell = 10;

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
        string[] detailAction = new string[] { "사용 시 HP를 65 회복한다."};
        int power = 65;

        List<EffectAtk> effectAtkList = new List<EffectAtk>();
        List<IStatusEffect> effectList = new List<IStatusEffect>();

        public int ActionType { get { return actionType; } }
        public string[] DetailAction { get { return detailAction; } }
        public int Power { get { return power; } }

        public List<EffectAtk> EffectAtkList { get { return effectAtkList; } }
        public List<IStatusEffect> EffectList { get { return effectList; } }

        public bool AttemptAction(ICharacter user)
        {
            if (user.HpCur < user.HpMax)
                return true;
            else
                return false;
        }

        public ResultBlock UseAction(ICharacter user, ICharacter target)
        {
            return new ResultBlock(0, power, null, null, 0);
        }
    }
}
