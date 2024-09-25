using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Item;

namespace TextRPG_Project2nd.Item.Cloth
{
    internal class LeaderArmor : IItem, ICloth
    {
        // IItem 구성 요소
        int itemType = 1;
        int itemID = 102;
        string name = "가죽갑옷";
        string[] detailItem = new string[] { "가죽이지만 의외로 무겁다.",
                                            "그래도 철제보다는 가벼우니까.",
                                            "그렇게 말하면서 나아갔던 이들이 있었다.",
                                            "그들은 지금 여기에 없다." };

        int priceBuy = 500;
        int priceSell = 100;

        public int ItemType { get { return itemType; } }
        public int ItemID { get { return itemID; } }
        public string Name { get { return name; } }
        public string[] DetailItem { get { return detailItem; } }

        public int PriceBuy { get { return priceBuy; } }
        public int PriceSell { get { return priceSell; } }

        // ICloth 구성 요소
        int[] attribute = new int[] { 135, 90, 75 };

        int[] defStatus = new int[] { 10, 10, 10 };

        public int[] Attribute { get { return attribute; } }

        public int[] DefStatus { get { return defStatus; } }
    }
}
