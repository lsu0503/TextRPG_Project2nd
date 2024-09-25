using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Item;

namespace TextRPG_Project2nd.System
{
    internal class Storage
    {
        public List<IItem> itemList = new List<IItem>();

        public List<IStackable> consumableList = new List<IStackable>();
        public List<ICloth> clothList = new List<ICloth>();
        public List<IWeapon> weaponList = new List<IWeapon>();
        public List<IStackable> currencyList = new List<IStackable>();

        public Storage()
        {
            itemList.Clear();
            UpdateStorage();
        }

        public void UpdateStorage()
        {
            consumableList.Clear();
            clothList.Clear();
            weaponList.Clear();
            currencyList.Clear();

            for(int i = 0; i < itemList.Count; i++)
            {
                switch (itemList[i].ItemType)
                {
                    case 0:
                        consumableList.Add(itemList[i] as IStackable);
                        break;
                    case 1:
                        clothList.Add(itemList[i] as ICloth);
                        break;
                    case 2:
                        weaponList.Add(itemList[i] as IWeapon);
                        break;
                }
            }
        }

        public void GetItem(IItem _item)
        {
            if(_item.ItemType == 0)
            {
                if (itemList.Exists(id => id.ItemID == _item.ItemID))
                {
                    (itemList.Find(id => id.ItemID == _item.ItemID) as IStackable).Amount++;
                }

                else
                {
                    itemList.Add(_item);
                    (itemList[itemList.Count - 1] as IStackable).Amount++;
                }
            }

            else
                itemList.Add(_item);

            UpdateStorage();
        }

        public void GetItemConsumable(IItem _item, int _amount)
        {
            if (itemList.Exists(id => id.ItemID == _item.ItemID))
            {
                (itemList.Find(id => id.ItemID == _item.ItemID) as IStackable).Amount += _amount;
            }

            else
            {
                itemList.Add(_item);
                (itemList[itemList.Count - 1] as IStackable).Amount += _amount;
            }

            UpdateStorage();
        }

        public bool RemoveItem(int index)
        {
            bool result = false;

            if (itemList[index].ItemType == 0)
            {
                (itemList[index] as IStackable).Amount--;

                if ((itemList[index] as IStackable).Amount == 0)
                {
                    itemList.RemoveAt(index);
                    result = true;
                }
            }

            else
            {
                itemList.RemoveAt(index);
                result = true;
            }

            UpdateStorage();
            return result;
        }

        public bool RemoveItem(int index, int category)
        {
            bool result = false;

            switch (category)
            {
                case 0:
                    consumableList[index].Amount--;

                    if (consumableList[index].Amount == 0)
                    {
                        itemList.RemoveAt(itemList.FindIndex(id => ReferenceEquals(id, consumableList[index])));
                        result = true;
                    }
                    break;

                case 1:
                    itemList.RemoveAt(itemList.FindIndex(id => ReferenceEquals(id, clothList[index])));
                    result = true;
                    break;

                case 2:
                    itemList.RemoveAt(itemList.FindIndex(id => ReferenceEquals(id, weaponList[index])));
                    result = true;
                    break;
            }
            
            UpdateStorage();
            return result;
        }

        public bool RemoveItemConsumable(int index, int amount)
        {
            bool result = false;
            
            consumableList[index].Amount -= amount;

            if (consumableList[index].Amount == 0)
            {
                itemList.RemoveAt(itemList.FindIndex(id => ReferenceEquals(id, consumableList[index])));
                result = true;
            }

            UpdateStorage();
            return result;
        }
    }
}
