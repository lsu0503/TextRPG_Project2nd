using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Project2nd.Scene
{
    internal class TitleScene
    {
        public int optionCur = 0;

        public void StartScene()
        {
            while (GameManager.Instance().SceneCurrent == GameManager.SceneNum.TitleScene)
            {
                Console.Clear();
                DisplayTitle(optionCur);
                GetInput();
            }
        }

        public void DisplayTitle(int optionNum)
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("               Wonder to the Fallen Stella");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Enter 'Z' to Select Option\n\n");

            if (optionNum == 0)
            {
                Console.WriteLine("▶ 시작하기");
                Console.WriteLine("   종료하기");
            }

            if (optionNum == 1)
            {
                Console.WriteLine("   시작하기");
                Console.WriteLine("▶ 종료하기");
            }
        }

        public void GetInput()
        {
            ConsoleKeyInfo input;
            input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    optionCur--;
                    if (optionCur < 0)
                        optionCur = 1;
                    break;

                case ConsoleKey.DownArrow:
                    optionCur++;
                    if (optionCur > 1)
                        optionCur = 0;
                    break;

                case ConsoleKey.Z:
                    if (optionCur == 0)
                        GameManager.Instance().SceneCurrent = GameManager.SceneNum.CreateScene;
                    else if (optionCur == 1)
                        GameManager.Instance().GameEnd();
                    break;
            }
        }
    }
}
