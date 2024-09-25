using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.Item;
using TextRPG_Project2nd.Item.Cloth;

namespace TextRPG_Project2nd.Scene
{
    internal class MainScene
    {
        int screenCurrent = 0;
        int screenCurrent2 = 0;
        
        int optionCur = 0;
        int optionCur2 = 0;
        int optionCur3 = 0;
        bool optionFlag = false;

        int optionMax = 0;
        int optionMax2 = 0;
        int optionMax3 = 0;
        
        public void StartScene()
        {
            screenCurrent = 0;
            screenCurrent2 = 0;

            optionCur = 0;
            optionCur2 = 0;
            optionCur3 = 0;
            optionFlag = false;

            optionMax = 0;
            optionMax2 = 0;
            optionMax3 = 0;

            while (GameManager.Instance().SceneCurrent == GameManager.SceneNum.MainScene)
            {
                Console.Clear();
                optionMax = 0;
                optionMax2 = 0;
                optionMax3 = 0;

                Console.SetCursorPosition(53 - GameManager.Instance().amber.ToString().Length, 3);
                Console.Write($"Amber: {GameManager.Instance().amber}");

                Console.SetCursorPosition(42, 4);
                Console.Write("Z: OK    X: Cancel");

                new DisplayCollection().DisplayCharaInfo(118, 0);

                Console.SetCursorPosition(0, 0);

                switch (screenCurrent)
                {
                    case 0:
                        DisplayVillage();
                        break;

                    case 1:
                        DisplayShop();
                        break;

                    case 2:
                        DisplayStorage();
                        break;

                    case 3:
                        DisplayDungeon(false);
                        break;

                    case -3:
                        DisplayDungeon(true);
                        break;
                }
            }
        }

        public void DisplayVillage()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("                        조사대 집결지");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("\n어디로 가시겠습니까?\n");

            string[] menuList = { "상점", "보관함", "던전", "게임 종료" };
            optionMax = menuList.Length;

            for (int i = 0; i < menuList.Length; i++)
            {
                if (optionCur == i)
                    Console.Write("▶ ");
                else
                    Console.Write("   ");

                Console.WriteLine(menuList[i]);
            }

            // 입력 인식
            switch (GetInput())
            {
                case 'Z':
                    if (optionCur != menuList.Length - 1)
                        screenCurrent = optionCur + 1;
                    else
                        Environment.Exit(0);

                    optionCur = 0;
                    break;

                case 'X':
                    optionCur = menuList.Length - 1;
                    break;
            }
        }

        public void DisplayShop()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("                        도그버크 상회");
            Console.WriteLine("------------------------------------------------------------");

            switch (screenCurrent2)
            {
                case 0:
                    DisplayShopEntrance();
                    break;

                case 1:
                    DisplayShopBuy(false);
                    break;

                case -1:
                    DisplayShopBuy(true);
                    break;

                case 2:
                    DisplayShopSell();
                    break;
            }
        }

        public void DisplayShopEntrance()
        {
            Console.WriteLine("\n주인: 무엇을 찾으시나요?\n");
            string[] menuList = { "구매하기", "판매하기", "나가기" };
            optionMax = menuList.Length;

            for (int i = 0; i < menuList.Length; i++)
            {
                if (optionCur == i)
                    Console.Write("▶ ");
                else
                    Console.Write("   ");

                Console.WriteLine(menuList[i]);
            }

            // 입력 인식
            switch (GetInput())
            {
                case 'Z':
                    if (optionCur != 2)
                        screenCurrent2 = optionCur + 1;
                    else
                    {
                        screenCurrent = 0;
                        screenCurrent2 = 0;
                    }
                    optionCur = 0;
                    break;

                case 'X':
                    screenCurrent = 0;
                    screenCurrent2 = 0;
                    optionCur = 0;
                    break;
            }
        }

        public void DisplayShopBuy(bool isMoneyShort)
        {
            Console.WriteLine("\n주인: 무엇을 구매하시겠습니까?\n");

            optionMax = GameManager.Instance().shop.merchandiseList.Count;

            for (int i = 0; i < GameManager.Instance().shop.merchandiseList.Count; i++)
            {
                if (optionCur == i)
                    Console.Write("▶ ");
                else
                    Console.Write("   ");

                Console.Write(GameManager.Instance().shop.merchandiseList[i].item.Name);

                if (GameManager.Instance().shop.merchandiseList[i].amount > 0)
                    Console.WriteLine($"({GameManager.Instance().shop.merchandiseList[i].amount})");

                Console.SetCursorPosition(24, 6 + i);
                Console.WriteLine($"{GameManager.Instance().shop.merchandiseList[i].item.PriceBuy}");
            }

            // 아이템 정보 표시
            new DisplayCollection().DisplayItemInfo(GameManager.Instance().shop.merchandiseList[optionCur].item);

            if (isMoneyShort)
                Console.WriteLine("\n돈이 부족합니다.");

            // 입력 인식
            switch (GetInput())
            {
                case 'Z':
                    if (GameManager.Instance().amber < GameManager.Instance().shop.merchandiseList[optionCur].item.PriceBuy)
                        screenCurrent2 = -1;

                    else
                    {
                        screenCurrent2 = 1;
                        GameManager.Instance().amber -= GameManager.Instance().shop.merchandiseList[optionCur].item.PriceBuy;
                        GameManager.Instance().storage.GetItem(GameManager.Instance().shop.merchandiseList[optionCur].item);
                        bool lastAmount = GameManager.Instance().shop.BuyItem(optionCur, 1);
                        if (lastAmount && optionCur >= GameManager.Instance().storage.itemList.Count)
                            optionCur--;
                    }
                    break;

                case 'X':
                    screenCurrent2 = 0;
                    optionCur = 0;
                    break;
            }
        }

        public void DisplayShopSell()
        {
            Console.WriteLine("\n주인: 무엇을 판매하시겠습니까?\n");
            optionMax = GameManager.Instance().storage.itemList.Count;

            for (int i = 0; i < GameManager.Instance().storage.itemList.Count; i++)
            {
                if (optionCur == i)
                    Console.Write("▶ ");
                else
                    Console.Write("   ");

                Console.Write(GameManager.Instance().storage.itemList[i].Name);

                if (GameManager.Instance().storage.itemList[i].ItemType == 0)
                {
                    IItem tempItem = GameManager.Instance().storage.itemList[i];
                    IStackable tempUsable = (IStackable)tempItem;
                    Console.WriteLine($"({tempUsable.Amount})");
                }

                if (ReferenceEquals(GameManager.Instance().storage.itemList[i], GameManager.Instance().player.Cloth) ||
                    ReferenceEquals(GameManager.Instance().storage.itemList[i], GameManager.Instance().player.Weapon))
                {
                    Console.SetCursorPosition(20, 6 + i);
                    Console.WriteLine("E");
                }

                Console.SetCursorPosition(24, 6 + i);
                Console.WriteLine($"{GameManager.Instance().storage.itemList[i].PriceSell}");
            }

            // 아이템 정보 표시
            new DisplayCollection().DisplayItemInfo(GameManager.Instance().storage.itemList[optionCur]);

            // 입력 인식
            switch (GetInput())
            {
                case 'Z':
                    GameManager.Instance().amber += GameManager.Instance().storage.itemList[optionCur].PriceSell;
                    GameManager.Instance().shop.SellItem(GameManager.Instance().storage.itemList[optionCur]);

                    // 장착 중인 장비 판매 시 장착 해제됨.
                    switch (optionCur2)
                    {
                        case 1:
                            if (ReferenceEquals(GameManager.Instance().player.Cloth, GameManager.Instance().storage.itemList[optionCur]))
                                GameManager.Instance().player.Cloth = null;
                            break;

                        case 2:
                            if (ReferenceEquals(GameManager.Instance().player.Weapon, GameManager.Instance().storage.itemList[optionCur]))
                                GameManager.Instance().player.Weapon = null;
                            break;
                    }

                    bool lastAmount = GameManager.Instance().storage.RemoveItem(optionCur);
                    if (lastAmount && optionCur >= GameManager.Instance().storage.itemList.Count)
                        optionCur--;
                    break;

                case 'X':
                    screenCurrent2 = 0;
                    optionCur = 0;
                    break;
            }
        }

        public void DisplayStorage()
        {
            int equipMax = 0;
            int storageMax = 0;

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("                           숙영지");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();

            string[] menuList = { "소모품 ", "방어구 ", "무 기  ", "성 리  " };
            optionMax2 = menuList.Length;

            for (int i = 0; i < menuList.Length; i++)
            {
                if (optionCur2 == i)
                    Console.Write("▶ ");
                else
                    Console.Write("   ");

                Console.Write(menuList[i]);
            }
            Console.WriteLine("\n");


            if (optionCur2 != 3) // 성리는 보관함을 사용하지 않기 떄문에 별개로 구성한다.
            {
                // 장착 슬롯 표시 양 산출
                switch (optionCur2)
                {
                    case 0:
                        if (optionFlag)
                            optionMax = GameManager.Instance().player.ConsumableList.Length;
                        else
                            optionMax = GameManager.Instance().storage.consumableList.Count;

                        equipMax = GameManager.Instance().player.ConsumableList.Length;
                        storageMax = GameManager.Instance().storage.consumableList.Count;
                        break;

                    case 1:
                        if (optionFlag)
                            optionMax = 1;
                        else
                            optionMax = GameManager.Instance().storage.clothList.Count;

                        equipMax = 1;
                        storageMax = GameManager.Instance().storage.clothList.Count;
                        break;

                    case 2:
                        if (optionFlag)
                            optionMax = 1;
                        else
                            optionMax = GameManager.Instance().storage.weaponList.Count;

                        equipMax = 1;
                        storageMax = GameManager.Instance().storage.weaponList.Count;
                        break;
                }

                optionMax3 = equipMax;

                // 장착 품목 표시
                for (int i = 0; i < equipMax; i++)
                {
                    if (equipMax == 1)
                        Console.Write($"E  ");
                    else
                        Console.Write($"E{i} ");

                    if (FindItemHolding(i) == null)
                        Console.Write("---------------");
                    else
                    {
                        Console.Write(FindItemHolding(i).Name);
                        if (optionCur2 == 0 && GameManager.Instance().player.ConsumableList[i] != null)
                            Console.Write($"({GameManager.Instance().player.ConsumableList[i].amount})");
                    }

                    if (optionCur3 == i)
                    {
                        Console.SetCursorPosition(19, 6 + i);
                        if (optionFlag)
                            Console.WriteLine("◀");
                        else
                            Console.WriteLine();
                    }
                    else
                        Console.WriteLine();
                }

                Console.WriteLine("====================");

                // 보관함 품목 표시
                for (int i = 0; i < storageMax; i++)
                {
                    if (optionCur == i)
                    {
                        if (!optionFlag)
                            Console.Write("▶ ");
                        else
                            Console.Write("▷ ");
                    }
                    else
                        Console.Write("   ");

                    Console.Write(FindItemStorage(i).Name);

                    if (optionCur2 == 0)
                        Console.WriteLine($"({(FindItemStorage(i) as IStackable).Amount})");

                    else
                    {
                        if (ReferenceEquals(FindItemStorage(i), FindItemHolding(0)))
                        {
                            Console.SetCursorPosition(20, 7 + equipMax + i);
                            Console.WriteLine("E");
                        }
                        else
                            Console.WriteLine();
                    }
                }

                // 아이템 정보 표시
                if (!optionFlag)
                {
                    if (storageMax > 0)
                        new DisplayCollection().DisplayItemInfo(FindItemStorage(optionCur));
                }

                else
                {
                    if (FindItemHolding(optionCur3) != null)
                        new DisplayCollection().DisplayItemInfo(FindItemHolding(optionCur3));
                }
            }

            // 성리 목록 표시 시.
            else
            {
                IPlayer target = GameManager.Instance().player;

                optionMax = 1;
                equipMax = 1;
                storageMax = 1;

                for (int i = 0; i < 1; i++)
                {
                    if (optionCur == i)
                        Console.Write("▶ ");
                    else
                        Console.Write("   ");

                    Console.WriteLine(target.Dogma.Name);
                }

                for (int i = 0; i < target.Dogma.DetailDogma.Length; i++)
                {
                    Console.SetCursorPosition(25, 6 + i);
                    Console.WriteLine(target.Dogma.DetailDogma[i]);
                }

                Console.SetCursorPosition(25, 13);
                Console.WriteLine((target.Dogma.Magic as IAction).Name);
                for (int i = 0; i < (target.Dogma.Magic as IAction).DetailAction.Length; i++)
                {
                    Console.SetCursorPosition(25, 14 + i);
                    Console.WriteLine((target.Dogma.Magic as IAction).DetailAction[i]);
                }
            }

            // 입력 인식
            switch (GetInput())
            {
                case 'Z':
                    if (storageMax > 0)
                    {
                        if (!optionFlag)
                            optionFlag = true;

                        else
                        {
                            switch (optionCur2)
                            {
                                case 0:
                                    bool isOver = false;

                                    int fillAmount = GameManager.Instance().player.EquipConsumable(FindItemStorage(optionCur) as IStackable, optionCur3, (FindItemStorage(optionCur) as IStackable).Amount);
                                    isOver = GameManager.Instance().storage.RemoveItemConsumable(optionCur, fillAmount);

                                    if (optionCur >= GameManager.Instance().storage.consumableList.Count && optionCur > 0)
                                        optionCur--;
                                    break;

                                case 1:
                                    GameManager.Instance().player.EquipItem(FindItemStorage(optionCur) as ICloth);
                                    break;

                                case 2:
                                    GameManager.Instance().player.EquipItem(FindItemStorage(optionCur) as IWeapon);
                                    break;
                            }

                            optionFlag = false;
                        }
                    }
                    break;

                case 'X':
                    if (optionFlag)
                        optionFlag = false;

                    else
                    {
                        screenCurrent = 0;
                        optionCur = 0;
                        optionCur2 = 0;
                        optionCur3 = 0;
                    }
                    break;
            }
        }

        // 보관함 아이템 찾는 함수.
        IItem FindItemStorage(int _index)
        {
            switch (optionCur2)
            {
                case 0:
                    return GameManager.Instance().storage.consumableList[_index] as IItem;

                case 1:
                    return GameManager.Instance().storage.clothList[_index] as IItem;

                case 2:
                    return GameManager.Instance().storage.weaponList[_index] as IItem;

                default:
                    return null;
            }
        }

        // 장착 아이템 찾는 함수.
        IItem FindItemHolding(int _index)
        {
            switch (optionCur2)
            {
                case 0:
                    if (GameManager.Instance().player.ConsumableList[_index] == null)
                        return null;
                    else
                        return GameManager.Instance().player.ConsumableList[_index].consumable as IItem;
                case 1:
                    return GameManager.Instance().player.Cloth as IItem;
                case 2:
                    return GameManager.Instance().player.Weapon as IItem;
                default:
                    return null;
            }
        }

        public void DisplayDungeon(bool isNotCleared)
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("                           회의장");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("\n눈 앞에 던전의 지도가 펼쳐져 있다...\n");

            string[] menuList = { "수사 - [쉬  움]", "조사 - [보  통]", "탐사 - [어려움]", "서사 - [최종장]" };
            optionMax = menuList.Length;
            optionMax2 = 0;

            for (int i = 0; i < menuList.Length; i++)
            {
                if (optionCur == i)
                    Console.Write("▶ ");
                else
                    Console.Write("   ");

                Console.WriteLine(menuList[i]);

                if (i < 3 && GameManager.Instance().isCleared[i] == true)
                {
                    Console.SetCursorPosition(24, 6 + i);
                    Console.WriteLine("○");
                }
            }
            Console.WriteLine("\n");

            new DisplayCollection().DisplayDifficulty(optionCur);

            if(isNotCleared)
                Console.WriteLine("\n이전 단계를 클리어해야 진입 가능합니다.");

            switch (GetInput())
            {
                case 'Z':
                    if (optionCur == 0 || GameManager.Instance().isCleared[optionCur - 1])
                        GameManager.Instance().SceneCurrent = GameManager.SceneNum.DungeonScene;
                    else
                        screenCurrent = -3;
                    break;

                case 'X':
                    screenCurrent = 0;
                    optionCur = 0;
                    break;
            }
        }

        // 입력 인식 함수
        public char GetInput()
        {
            ConsoleKeyInfo input;
            input = Console.ReadKey();

            // 화살표는 자동 진행.
            switch (input.Key)
            {
                case ConsoleKey.DownArrow:
                    if (!optionFlag)
                    {
                        optionCur++;
                        if (optionCur >= optionMax)
                            optionCur = 0;
                    }
                    else
                    {
                        optionCur3++;
                        if (optionCur3 >= optionMax)
                            optionCur3 = 0;
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (!optionFlag)
                    {
                        optionCur--;
                        if (optionCur < 0)
                            optionCur = optionMax - 1;
                    }
                    else
                    {
                        optionCur3--;
                        if (optionCur3 < 0)
                            optionCur = optionMax3 - 1;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (optionMax2 > 0)
                    {
                        optionCur2++;
                        if (optionCur2 >= optionMax2)
                            optionCur2 = 0;
                        optionCur = 0;
                        optionCur3 = 0;

                        optionFlag = false;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (optionMax2 > 0)
                    {
                        optionCur2--;
                        if (optionCur2 < 0)
                            optionCur2 = optionMax2 - 1;
                        optionCur = 0;
                        optionCur3 = 0;

                        optionFlag = false;
                    }
                    break;

                // 화살표 이외의 입력값은 해당 함수에서 직접 처리
                case ConsoleKey.Z:
                    return 'Z';

                case ConsoleKey.X:
                    return 'X';
            }

            return '\0';
        }
    }
}
