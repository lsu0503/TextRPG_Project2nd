using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Item;

namespace TextRPG_Project2nd.Item.Cloth
{
    internal class WondererCloth : IItem, ICloth
    {
        // IItem 구성 요소
        int itemType = 1;
        int itemID = 101;
        string name = "모험가 복장";
        string[] detailItem = new string[] { "오랫동안 사용하여 너덜너덜해진 옷",
                                                "그래도 아직은 쓸만하다.",
                                                "얼마 남지 않은 추억이기에, 버릴 수는 없는 것이다.",
                                                "분명 버려야 함에도..." };

        int priceBuy = 500;
        int priceSell = 100;

        public int ItemType { get { return itemType; } }
        public int ItemID { get { return itemID; } }
        public string Name { get { return name; } }
        public string[] DetailItem { get { return detailItem; } }

        public int PriceBuy { get { return priceBuy; } }
        public int PriceSell { get { return priceSell; } }

        // ICloth 구성 요소
        int[] attribute = new int[] { 100, 100, 100 };

        int[] defStatus = new int[] { 10, 10, 10 };

        public int[] Attribute { get { return attribute; } }

        public int[] DefStatus { get { return defStatus; } }
    }
}
