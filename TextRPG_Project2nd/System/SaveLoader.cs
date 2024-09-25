using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.Dogma;
using TextRPG_Project2nd.Item;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace TextRPG_Project2nd.System
{
    public struct SaveIndex
    {
        public List<string[]> shopItemList { get; set; } = new List<string[]>();// Merchandise -> int[] = {itemID, amount}
        public List<string[]> storageConsumableList { get; set; } = new List<string[]>(); // IItem -> int = itemID
        public List<string> storageClothList { get; set; } = new List<string>(); // IItem -> int = itemID
        public List<string> storageWeaponList { get; set; } = new List<string>(); // IItem -> int = itemID

        public string playerName { get; set; }
        public string playerLevel { get; set; }
        public string playerExp { get; set; }

        public string playerCloth { get; set; }
        public string playerWeapon { get; set; }
        public string[] playerConsumablesId { get; set; } = new string[3]; // CounsumableSlot -> ID와 개수로 쪼개넣기.
        public string[] playerConsumablesAmount { get; set; } = new string[3];

        public string playerDogma { get; set; }

        public string saveAmber { get; set; }
        public string[] saveIsCleared { get; set; } = new string[4];

        public SaveIndex(List<Merchandise> _shopItemList, List<IStackable> _storageConsumableList,
                         List<ICloth> _storageClothList, List<IWeapon> _storageWeaponList,
                         string _playerName, int _playerLevel, int _plaerExp,
                         int _playerCloth, int _playerWeapon, ConsumableSlot[] _playerConsumables, int _playerDogma,
                         int _saveAmber, bool[] _saveIsCleared)
        {
            shopItemList.Clear();
            for (int i = 0; i < _shopItemList.Count; i++)
                shopItemList.Add(new string[] { _shopItemList[i].item.ItemID.ToString(), _shopItemList[i].amount.ToString() });

            storageConsumableList.Clear();
            for (int i = 0; i < _storageConsumableList.Count; i++)
                storageConsumableList.Add(new string[] { (_storageConsumableList[i] as IItem).ItemID.ToString(), _storageConsumableList[i].Amount.ToString() });

            storageClothList.Clear();
            for (int i = 0; i < _storageClothList.Count; i++)
                storageClothList.Add((_storageClothList[i] as IItem).ItemID.ToString());

            storageWeaponList.Clear();
            for (int i = 0; i < _storageWeaponList.Count; i++)
                storageWeaponList.Add((_storageWeaponList[i] as IItem).ItemID.ToString());

            playerName = _playerName;
            playerLevel = _playerLevel.ToString();
            playerExp = _plaerExp.ToString();

            playerCloth = _playerCloth.ToString();
            playerWeapon = _playerWeapon.ToString();

            for (int i = 0; i < 3; i++)
            {
                if (_playerConsumables[i] != null)
                {
                    playerConsumablesId[i] = (_playerConsumables[i].consumable as IItem).ItemID.ToString();
                    playerConsumablesAmount[i] = _playerConsumables[i].amount.ToString();
                }

                else
                {
                    playerConsumablesId[i] = (-1).ToString();
                    playerConsumablesAmount[i] = (-1).ToString();
                }
            }

            playerDogma = _playerDogma.ToString();

            saveAmber = _saveAmber.ToString();

            for(int i = 0; i < _saveIsCleared.Length; i++)
                saveIsCleared[i] = _saveIsCleared[i].ToString();
        }
    }

    internal class SaveLoader
    {
        public void SaveGame(int index)
        {
            IPlayer tempPlayer = GameManager.Instance().player as IPlayer;

            // 세이브 내용 불러오기
            SaveIndex tempSave = new SaveIndex(GameManager.Instance().shop.merchandiseList, GameManager.Instance().storage.consumableList,
                                               GameManager.Instance().storage.clothList, GameManager.Instance().storage.weaponList,
                                               (tempPlayer as ICharacter).Name, (tempPlayer as ICharacter).Level, tempPlayer.ExpCur,
                                               (tempPlayer.Cloth as IItem).ItemID, (tempPlayer.Weapon as IItem).ItemID, tempPlayer.ConsumableList,
                                               tempPlayer.Dogma.DogmaID, GameManager.Instance().amber, GameManager.Instance().isCleared);

            // 디렉토리 지정
            string FilePath = Directory.GetCurrentDirectory() + "\\SaveData";
            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);

            // 세이브 시작
            string SaveString = JsonSerializer.Serialize(tempSave);
            File.WriteAllText(Path.Combine(FilePath, $"SaveData{index}.json"), SaveString);
        }

        public SaveIndex LoadGame(int index)
        {
            SaveIndex saveData = new SaveIndex();
            string LoadString;
            //Link
            string FilePath = Directory.GetCurrentDirectory() + $"\\SaveData\\SaveData{index}.json";

            //Load
            if (File.Exists(FilePath))
            {
                LoadString = File.ReadAllText(FilePath);
                saveData = JsonSerializer.Deserialize<SaveIndex>(LoadString);
            }

            return saveData;
        }

        public bool CheckSave(int index)
        {
            //Link
            string FilePath = Directory.GetCurrentDirectory() + $"\\SaveData\\SaveData{index}.json";

            //Load
            if (File.Exists(FilePath))
                return true;
            else
                return false;
        }

        public void AdjustSave(SaveIndex _save)
        {
            IdCollection idDict = new IdCollection();

            GameManager.Instance().shop.merchandiseList.Clear();
            for (int i = 0; i < _save.shopItemList.Count; i++)
            {
                Merchandise tempMerchandise = new Merchandise(idDict.itemCollection.Find(id => id.ItemID == Convert.ToInt32(_save.shopItemList[i][0])),
                                                              Convert.ToInt32(_save.shopItemList[i][1]));
                GameManager.Instance().shop.merchandiseList.Add(tempMerchandise);
            }

            GameManager.Instance().storage.itemList.Clear();
            GameManager.Instance().storage.UpdateStorage();

            for (int i = 0; i < _save.storageConsumableList.Count; i++)
            {
                IItem tempConsumable = idDict.itemCollection.Find(id => id.ItemID == Convert.ToInt32(_save.storageConsumableList[i][0]));
                GameManager.Instance().storage.GetItemConsumable(tempConsumable, Convert.ToInt32(_save.storageConsumableList[i][1]));
            }

            for (int i = 0; i < _save.storageClothList.Count; i++)
            {
                IItem tempCloth = idDict.itemCollection.Find(id => id.ItemID == Convert.ToInt32(_save.storageClothList[i]));
                GameManager.Instance().storage.GetItem(tempCloth);
            }

            for (int i = 0; i < _save.storageWeaponList.Count; i++)
            {
                IItem tempWeapon = idDict.itemCollection.Find(id => id.ItemID == Convert.ToInt32(_save.storageWeaponList[i]));
                GameManager.Instance().storage.GetItem(tempWeapon);
            }

            ICharacter player = GameManager.Instance().player;

            player.Name = _save.playerName;
            player.Level = Convert.ToInt32(_save.playerLevel);
            (player as IPlayer).ExpCur = 0;
            (player as IPlayer).GetExp(Convert.ToInt32(_save.playerExp));

            (player as IPlayer).Cloth = idDict.itemCollection.Find(id => id.ItemID == Convert.ToInt32(_save.playerCloth)) as ICloth;
            (player as IPlayer).Weapon = idDict.itemCollection.Find(id => id.ItemID == Convert.ToInt32(_save.playerWeapon)) as IWeapon;

            for(int i = 0; i < (player as IPlayer).ConsumableList.Length; i++)
            {
                if (Convert.ToInt32(_save.playerConsumablesId[i]) > 0)
                {
                    IStackable tempStackable = idDict.itemCollection.Find(id => id.ItemID == Convert.ToInt32(_save.playerConsumablesId[i])) as IStackable;
                    (player as IPlayer).ConsumableList[i] = new ConsumableSlot(tempStackable, Convert.ToInt32(_save.playerConsumablesAmount[i]));
                }
                else
                {
                    (player as IPlayer).ConsumableList[i] = null;
                }
            }

            (player as IPlayer).Dogma = idDict.dogmaCollection.Find(id => id.DogmaID == Convert.ToInt32(_save.playerDogma));

            player.UpdateAttribute();

            GameManager.Instance().amber = Convert.ToInt32(_save.saveAmber);
            
            for(int i = 0; i < _save.saveIsCleared.Length; i++)
                GameManager.Instance().isCleared[i] = Convert.ToBoolean(_save.saveIsCleared[i]);
        }
    }
}
