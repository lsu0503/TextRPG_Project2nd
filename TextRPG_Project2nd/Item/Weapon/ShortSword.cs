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
    public class ShortSword : IItem, IWeapon
    {
        // IItemBase 구성 요소
        int itemType = 2;
        int itemID = 202;
        string name = "보급용 직검";
        string[] detailItem = new string[] { "지금은 보기 힘들지만, 예전에는 다들 이걸 썼었다.",
                                             "그도 그럴게, 땅에 널려있었으니까.",
                                             "하지만 지금은, 이 마저도 얼마 남지 않았다.",
                                             "마치 우리들 처럼." };

        int priceBuy = 500;
        int priceSell = 100;

        public int ItemType { get { return itemType; } }
        public int ItemID { get { return itemID; } }
        public string Name { get { return name; } }
        public string[] DetailItem { get { return detailItem; } }

        public int PriceBuy { get { return priceBuy; } }
        public int PriceSell { get { return priceSell; } }

        // IWeapon 구성 요소
        int[] typeAtk = { 110, 120, 70 }; // 0: 물리 공격력 | 1: 스킬 공격력 | 2: 마법 공격력
        IAttack attack = new AttackBase("공격", 10, null, 1);
        ISkill skill = new SkillBash();

        public int[] TypeAtk { get { return typeAtk; } }

        public IAttack Attack { get { return attack; } }

        public ISkill Skill { get { return skill; } }
    }
}
