using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.Dogma;
using TextRPG_Project2nd.Item;

namespace TextRPG_Project2nd.System
{
    public struct SaveIndex
    {
        public List<int[]> shopItemList = new List<int[]>(); // Merchandise -> int[] = {itemID, amount}
        public List<int> storageItemList = new List<int>(); // IItem -> int = itemID

        public int playerLevel;
        public int playerExp;

        public ICloth playerCloth;
        public IWeapon playerWeapon;
        public int[,] playerConsumables = new int[3,2]; // CounsumableSlot -> int[,] = {{itemID, amount}, ...}
        public IDogma playerDogma;

        public int saveAmber;
        public bool[] saveIsCleared = new bool[4];

        public SaveIndex(List<Merchandise> _shopItemList, List<IItem> _storageItemList,
                         int _playerLevel, int _plaerExp, 
                         ICloth _playerCloth, IWeapon _playerWeapon, ConsumableSlot[] _playerConsumables, IDogma _playerDogma,
                         int _saveAmber, bool[] _saveIsCleared)
        {
            for(int i = 0; i < _shopItemList.Count; i++)
                shopItemList.Add(new int[] { _shopItemList[i].item.ItemID, _shopItemList[i].amount });

            for (int i = 0; i < _storageItemList.Count; i++)
                storageItemList.Add(_storageItemList[i].ItemID);

            playerLevel = _playerLevel;
            playerExp = _plaerExp;

            playerCloth = _playerCloth;
            playerWeapon = _playerWeapon;
            
            for(int i = 0; i < 3; i++)
            {
                playerConsumables[i, 0] = (_playerConsumables[i].consumable as IItem).ItemID;
                playerConsumables[i, 1] = _playerConsumables[i].consumable.Amount;
            }

            playerDogma = _playerDogma;

            saveAmber = _saveAmber;
            saveIsCleared = _saveIsCleared.ToArray();
        }
    }

    internal class SaveLoader
    {
        public void SaveGame(int index)
        {
            IPlayer tempPlayer = GameManager.Instance().player as IPlayer;

            // 세이브 내용 불러오기
            SaveIndex tempSave = new SaveIndex(GameManager.Instance().shop.merchandiseList, GameManager.Instance().storage.itemList,
                                               (tempPlayer as ICharacter).Level, tempPlayer.ExpCur,
                                               tempPlayer.Cloth, tempPlayer.Weapon, tempPlayer.ConsumableList, tempPlayer.Dogma,
                                               GameManager.Instance().amber, GameManager.Instance().isCleared);

            // 세이브 시작
        }

        public bool LoadGame(int index)
        {
            return false;
        }

        public bool CheckSave(int index)
        {
            return false;
        }
    }
}
