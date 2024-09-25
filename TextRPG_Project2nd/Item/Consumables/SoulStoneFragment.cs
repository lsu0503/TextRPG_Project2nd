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
    public class SoulStoneFragment : IItem, IStackable, IAction
    {
        // IItemBase 구성 요소
        int itemType = 0;
        int itemID = 081;
        string name = "영석 부스러기";
        string[] detailItem = new string[] { "약한 영석이 전투의 충격에 부스러진 것.",
                                                "약자란 것은 으레 부스러지기 마련이다." };

        int priceBuy = 300;
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
        string[] detailAction = new string[] { "상점에 팔아서 100Amber를 얻을 수 있다.",
                                               "혹은 사용하여 100Ember를 습득할 수 있다."};
        int power;

        List<EffectAtk> effectAtkList = new List<EffectAtk>();
        List<IStatusEffect> effectList = new List<IStatusEffect>();

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
            return new ResultBlock(0, 0, null, null, power);
        }
    }
}
