using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Dogma;

namespace TextRPG_Project2nd.Scene
{
    internal class CreateScene
    {
        int optionCur = 0;

        public void StartScene()
        {
            Console.Clear();
            InputName();
            while (GameManager.Instance().SceneCurrent == GameManager.SceneNum.CreateScene)
            {
                Console.Clear();
                DisplaySelectStella();
                GetInput();
            }
        }

        public void InputName()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("                당신의 이름을 정해주세요.");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Enter 'Enter' to enter name\n");

            string temp = Console.ReadLine();

            GameManager.Instance().player.Name = temp;
        }

        public void DisplaySelectStella()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("                당신의 성리를 정해주세요.");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Enter 'Z' to Select Option\n");

            for (int i = 0; i < GameManager.Instance().dogmaList.Count; i++)
            {
                if (optionCur == i)
                    Console.Write("▶ ");
                else
                    Console.Write("   ");

                Console.WriteLine(GameManager.Instance().dogmaList[i].dogma.Name);
            }

            for (int i = 0; i < GameManager.Instance().dogmaList[optionCur].dogma.DetailDogma.Length; i++)
            {
                Console.SetCursorPosition(30, 5 + i);
                Console.WriteLine(GameManager.Instance().dogmaList[optionCur].dogma.DetailDogma[i]);
            }

            Console.SetCursorPosition(30, 13);
            Console.WriteLine((GameManager.Instance().dogmaList[optionCur].dogma.Magic as IAction).Name);
            for (int i = 0; i < (GameManager.Instance().dogmaList[optionCur].dogma.Magic as IAction).DetailAction.Length; i++)
            {
                Console.SetCursorPosition(30, 14 + i);
                Console.WriteLine((GameManager.Instance().dogmaList[optionCur].dogma.Magic as IAction).DetailAction[i]);
            }
        }
        
        public void GetInput()
        {
            ConsoleKeyInfo input;
            input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.DownArrow:
                    optionCur++;
                    if (optionCur >= GameManager.Instance().dogmaList.Count)
                        optionCur = 0;
                    break;

                case ConsoleKey.UpArrow:
                    optionCur--;
                    if (optionCur < 0)
                        optionCur = GameManager.Instance().dogmaList.Count - 1;
                    break;

                case ConsoleKey.Z:
                    GameManager.Instance().player.Dogma = GameManager.Instance().dogmaList[optionCur].dogma;
                    GameManager.Instance().SceneCurrent = GameManager.SceneNum.MainScene;
                    break;
            }
        }
    }
}
