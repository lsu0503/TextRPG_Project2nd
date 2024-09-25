using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Item.Consumables;
using TextRPG_Project2nd.Item.Cloth;
using TextRPG_Project2nd.Item.Weapon;
using TextRPG_Project2nd.Item;

namespace TextRPG_Project2nd.System
{
    public class Merchandise
    {
        public IItem item;
        public int amount;

        public Merchandise(IItem _item, int _amount)
        {
            item = _item;
            amount = _amount; // 장비: 0(단일 판매, 표시 X), -1(무한 판매, 표시 X) | 소모품: 자연수(유한 판매, 표시O), -1(무한 판매. 표시X)
        }
    }

    internal class Shop
    {
        public List<Merchandise> merchandiseList = new List<Merchandise>();

        public Shop()
        {
            merchandiseList.Add(new Merchandise(new WondererCloth(), 0));
            merchandiseList.Add(new Merchandise(new WondererCloth(), 0));
            merchandiseList.Add(new Merchandise(new BluntRod(), 0));
            merchandiseList.Add(new Merchandise(new BluntRod(), 0));
            merchandiseList.Add(new Merchandise(new LeaderArmor(), 0));
            merchandiseList.Add(new Merchandise(new LeaderArmor(), 0));
            merchandiseList.Add(new Merchandise(new ShortSword(), 0));
            merchandiseList.Add(new Merchandise(new ShortSword(), 0));
            merchandiseList.Add(new Merchandise(new HealingPotion1(), -1));
            merchandiseList.Add(new Merchandise(new StrengthPotion(), 20));
            merchandiseList.Add(new Merchandise(new SoulStoneFragment(), -1));
        }

        public bool BuyItem(int _Index, int _amount)
        {
            if (merchandiseList[_Index].amount >= 0)
            {
                merchandiseList[_Index].amount -= _amount;

                if (merchandiseList[_Index].amount <= 0)
                {
                    merchandiseList.RemoveAt(_Index);
                    return true;
                }

                else
                    return false;
            }

            else
                return false;
        }

        public void SellItem(IItem _item)
        {
            if (_item.ItemType == 0)
            {
                if (merchandiseList.Exists(id => id.item.ItemID == _item.ItemID && id.amount > 0))
                {
                    merchandiseList.Find(id => id.item.ItemID == _item.ItemID && id.amount > 0).amount++;
                }

                else
                    merchandiseList.Add(new Merchandise(_item, 1));
            }

            else
                merchandiseList.Add(new Merchandise(_item, 0));
        }
    }
}
