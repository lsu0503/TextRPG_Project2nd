﻿using TextRPG_Project2nd.Scene;

namespace TextRPG_Project2nd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TitleScene titleScene = new TitleScene();
            CreateScene createScene = new CreateScene();
            MainScene mainScene = new MainScene();
            DungeonScene dungeonScene = new DungeonScene();

            while (true)
            {
                switch (GameManager.Instance().SceneCurrent)
                {
                    case GameManager.SceneNum.TitleScene:
                        titleScene.StartScene();
                        break;

                    case GameManager.SceneNum.CreateScene:
                        createScene.StartScene();
                        break;

                    case GameManager.SceneNum.MainScene:
                        mainScene.StartScene();
                        break;

                    case GameManager.SceneNum.DungeonScene:
                        dungeonScene.StartScene();
                        break;
                }
            }
        }
    }
}
