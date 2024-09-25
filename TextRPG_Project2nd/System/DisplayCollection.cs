using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.Item;

namespace TextRPG_Project2nd.System
{
    internal class DisplayCollection
    {
        public void DisplayItemInfo(IItem _item)
        {
            for (int i = 0; i < _item.DetailItem.Length; i++)
            {
                Console.SetCursorPosition(30, 6 + i);
                Console.WriteLine(_item.DetailItem[i]);
            }

            switch (_item.ItemType)
            {
                case 0:
                case 3:
                    DisplayIUsableInfo(_item as IStackable);
                    break;

                case 1:
                    DisplayIClothInfo(_item as ICloth);
                    break;

                case 2:
                    DisplayIWeaponInfo(_item as IWeapon);
                    break;
            }
        }

        public void DisplayIUsableInfo(IStackable _item)
        {
            for (int i = 0; i < (_item as IAction).DetailAction.Length; i++)
            {
                Console.SetCursorPosition(30, 13 + i);
                Console.WriteLine((_item as IAction).DetailAction[i]);
            }
        }

        public void DisplayIClothInfo(ICloth _item)
        {
            Console.SetCursorPosition(30, 13);
            Console.Write("생명: " + "{0, -3}  ", _item.Attribute[0]);
            Console.Write("정신: " + "{0, -3}  ", _item.Attribute[1]);
            Console.Write("영혼: " + "{0, -3}", _item.Attribute[2]);
        }

        public void DisplayIWeaponInfo(IWeapon _item)
        {
            Console.SetCursorPosition(30, 13);
            Console.Write("공격 위력: {0, -3}      공격 간격: {0, 2}", (_item.Attack as IAction).Power, _item.Attack.TurnCounterMax);

            Console.SetCursorPosition(30, 15);
            Console.Write("물리 공격력: " + "{0, -3}  ", _item.TypeAtk[0]);

            Console.SetCursorPosition(30, 16);
            Console.Write("마법 공격력: " + "{0, -3}", _item.TypeAtk[1]);

            Console.SetCursorPosition(30, 19);
            Console.Write($"{(_item.Skill as IAction).Name}");

            for (int i = 0; i < (_item.Skill as IAction).DetailAction.Length; i++)
            {
                Console.SetCursorPosition(30, 20 + i);
                Console.Write($"{(_item.Skill as IAction).DetailAction[i]}");
            }
        }

        public void DisplayDifficulty(int _difficulty)
        {
            switch (_difficulty)
            {
                case 0:
                    Console.SetCursorPosition(30, 6);
                    Console.Write("우리에게 이곳은 아직 미지의 장소다.");
                    Console.SetCursorPosition(30, 7);
                    Console.Write("만전을 기하는 것도 좋겠지.");
                    Console.SetCursorPosition(30, 9);
                    Console.Write("최대 진행 단계: 5");
                    Console.SetCursorPosition(30, 10);
                    Console.WriteLine("기준 레벨: 0");
                    break;

                case 1:
                    Console.SetCursorPosition(30, 6);
                    Console.Write("여기에서 올라오는 기운은 아직 강하다.");
                    Console.SetCursorPosition(30, 7);
                    Console.Write("우리가 스러지기 전에, 성공해야 한다...");
                    Console.SetCursorPosition(30, 9);
                    Console.Write("최대 진행 단계: 12");
                    Console.SetCursorPosition(30, 10);
                    Console.WriteLine("기준 레벨: 1");
                    break;

                case 2:
                    Console.SetCursorPosition(30, 6);
                    Console.Write("이젠 정상적으로 움직일 수 있는 건 나 뿐이다.");
                    Console.SetCursorPosition(30, 7);
                    Console.Write("지체할 시간이 없다. 부디, 늦지 않기를...");
                    Console.SetCursorPosition(30, 9);
                    Console.Write("최대 진행 단계: 32");
                    Console.SetCursorPosition(30, 10);
                    Console.WriteLine("기준 레벨: 2");
                    break;

                case 3:
                    Console.SetCursorPosition(30, 6);
                    Console.Write("모두가 나에게 말한다. 무리하지 말라고.");
                    Console.SetCursorPosition(30, 7);
                    Console.Write("그렇게 말한 동료가, 방금 웃는 얼굴로 사라져버렸다...");
                    Console.SetCursorPosition(30, 9);
                    Console.Write("최대 진행 단계: 50");
                    Console.SetCursorPosition(30, 10);
                    Console.WriteLine("기준 레벨: 3");
                    break;
            }
        }

        // 플레이어 캐릭터 정보 표시
        public void DisplayCharaInfo(int xPos, int yPos)
        {
            string tempString = null;
            ICharacter target = GameManager.Instance().player;

            // 한글은 2칸을 차지하므로 x좌표는 한글의 수 만큼 추가로 감소시켜야 한다.
            if (target.Level < 5)
                tempString = string.Format($"{target.Name} Lv.{target.Level} [{0,5:N2}%]", (target as IPlayer).ExpCur / (float)(target as IPlayer).ExpMax);
            else
                tempString = string.Format($"{target.Name} Lv.{target.Level} [-----]");
            Console.SetCursorPosition(xPos - tempString.Length, yPos);
            Console.Write(tempString);

            tempString = string.Format("생명: {0, 3}   정신: {0, 3}   영혼: {0, 3}", target.Attribute[0], target.Attribute[1], target.Attribute[2]);
            Console.SetCursorPosition(xPos - tempString.Length - 6, yPos + 1);
            Console.Write(tempString);

            tempString = string.Format("물리 공격력: {0,3}", target.TypeAtk[0]);
            Console.SetCursorPosition(xPos - tempString.Length - 5, yPos + 2);
            Console.Write(tempString);

            tempString = string.Format("마법 공격력: {0,3}", target.TypeAtk[1]);
            Console.SetCursorPosition(xPos - tempString.Length - 5, yPos + 3);
            Console.Write(tempString);
        }
    }
}
