using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.System;

namespace TextRPG_Project2nd.Scene
{
    internal class TitleScene
    {
        public int optionCur = 0;
        public int screenCur = 0;

        public void StartScene()
        {
            while (GameManager.Instance().SceneCurrent == GameManager.SceneNum.TitleScene)
            {
                Console.Clear();
                switch (screenCur)
                {
                    case 0:
                        DisplayTitle();
                        break;

                    case 1:
                        DisplayLoad(false);
                        break;

                    case 2:
                        DisplayLoad(true);
                        break;

                    case 3:
                        DisplayLoad(false);
                        break;
                }
            }
        }

        public void DisplayTitle()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("               Wonder to the Fallen Stella");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Enter 'Z' to Select Option\n\n");

            string[] menuList = { "시작하기", "불러오기", "종료하기" };

            for (int i = 0; i < menuList.Length; i++)
            {
                if (optionCur == i)
                    Console.Write("▶ ");
                else
                    Console.Write("   ");

                Console.WriteLine(menuList[i]);
            }

            ConsoleKeyInfo input;
            input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    optionCur--;
                    if (optionCur < 0)
                        optionCur = menuList.Length - 1;
                    break;

                case ConsoleKey.DownArrow:
                    optionCur++;
                    if (optionCur > menuList.Length - 1)
                        optionCur = 0;
                    break;

                case ConsoleKey.Z:
                    switch (optionCur)
                    {
                        case 0:
                            screenCur = 3;
                            optionCur = 0;
                            break;
                        case 1:
                            screenCur = 1;
                            optionCur = 0;
                            break;
                        case 2:
                            GameManager.Instance().GameEnd();
                            break;
                    }
                    break;
            }
        }

        public void DisplayLoad(bool isEmptySave)
        {
            SaveLoader saveClass = new SaveLoader();
            bool[] saveExist = { saveClass.CheckSave(0), saveClass.CheckSave(1), saveClass.CheckSave(2) };

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("               Wonder to the Fallen Stella");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Enter 'Z' to Select Option\n\n");

            SaveIndex[] tempSave = new SaveIndex[3];
            for(int i = 0; i < tempSave.Length; i++)
            {
                if (saveExist[i])
                    tempSave[i] = saveClass.LoadGame(i);
            }

            for (int i = 0; i < tempSave.Length; i++)
            {
                if (optionCur == i)
                    Console.Write("▶ ");
                else
                    Console.Write("   ");

                if (saveExist[i])
                    Console.WriteLine(tempSave[i].playerName);
                else
                    Console.WriteLine("--------------");
            }

            if (isEmptySave)
                Console.WriteLine("\n비어있는 저장파일 입니다.");

            ConsoleKeyInfo input;
            input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    optionCur--;
                    if (optionCur < 0)
                        optionCur = tempSave.Length - 1;
                    break;

                case ConsoleKey.DownArrow:
                    optionCur++;
                    if (optionCur > tempSave.Length - 1)
                        optionCur = 0;
                    break;

                case ConsoleKey.Z:
                    if (screenCur != 3)
                    {
                        if (saveExist[optionCur])
                        {
                            GameManager.Instance().saveSlot = optionCur;
                            saveClass.AdjustSave(tempSave[optionCur]);
                            GameManager.Instance().SceneCurrent = GameManager.SceneNum.MainScene;
                        }

                        else
                            screenCur = 2;
                    }

                    else
                    {
                        if (saveExist[optionCur])
                        {
                            Console.WriteLine("이 저장파일에 덮어씌우시겠습니까? [Z: Yew / X: No]");

                            input = Console.ReadKey();
                            bool isTyped = false;

                            while (!isTyped)
                            {
                                switch (input.Key)
                                {
                                    case ConsoleKey.Z:
                                        GameManager.Instance().saveSlot = optionCur;
                                        GameManager.Instance().SceneCurrent = GameManager.SceneNum.CreateScene;
                                        isTyped = true;
                                        break;

                                    case ConsoleKey.X:
                                        isTyped = true;
                                        break;
                                }
                            }
                        }

                        else
                        {
                            GameManager.Instance().saveSlot = optionCur;
                            GameManager.Instance().SceneCurrent = GameManager.SceneNum.CreateScene;
                        }
                    }
                    break;
                case ConsoleKey.X:
                    screenCur = 0;
                    optionCur = 0;
                    break;
            }
        }
    }
}
