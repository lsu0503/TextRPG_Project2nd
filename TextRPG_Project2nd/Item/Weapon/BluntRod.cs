using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Action.Skill;
using TextRPG_Project2nd.Item;

namespace TextRPG_Project2nd.Item.Weapon
{
    public class BluntRod : IItem, IWeapon
    {
        // IItemBase 구성 요소
        int itemType = 2;
        int itemID = 201;
        string name = "뭉툭한 작대";
        string[] detailItem = new string[] { "이전 부터 사용해왔던 쓰기 편한 작대.",
                                                "손 때가 탄 물건은 으레 버리기 힘들기 마련이다.",
                                                "그나마 쓸 만 한 것이 위안인 것일까.",
                                                "그래도 때가 되면 버려야 할 것이다." };

        int priceBuy = 500;
        int priceSell = 100;

        public int ItemType { get { return itemType; } }
        public int ItemID { get { return itemID; } }
        public string Name { get { return name; } }
        public string[] DetailItem { get { return detailItem; } }

        public int PriceBuy { get { return priceBuy; } }
        public int PriceSell { get { return priceSell; } }

        // IWeapon 구성 요소
        int[] typeAtk = { 100, 100, 100 }; // 0: 물리 공격력 | 1: 스킬 공격력 | 2: 마법 공격력
        IAttack attack = new AttackBase("공격", 10, null, 1);
        ISkill skill = new SkillRelaxTension();

        public int[] TypeAtk { get { return typeAtk; } }

        public IAttack Attack { get { return attack; } }

        public ISkill Skill { get { return skill; } }
    }
}
