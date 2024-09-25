using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.Character.Monster;
using TextRPG_Project2nd.Item;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Scene
{
    public class ActionBlock
    {
        public IAction action;
        public ICharacter user;
        public ICharacter target;

        public ActionBlock(IAction _action, ICharacter _user, ICharacter _target)
        {
            action = _action;
            user = _user;
            target = _target;
        }
    }

    internal class DungeonScene
    {
        List<string> logList = new List<string>();

        int DepthCur = 0;
        int DepthMax = 0;
        ICharacter enemy = null;
        ICharacter player = GameManager.Instance().player;

        List<ActionBlock> actionList = new List<ActionBlock>();

        public void StartScene()
        {
            GameManager.Instance().ember = 1000;
            DepthCur = 0;

            do
            {
                DepthCur++;
                enemy = null;
                Console.Clear();

                switch (GameManager.Instance().gradeDifficulty)
                {
                    case 0:
                        StartEasy();
                        break;

                    case 1:
                        StartNormal();
                        break;

                    case 2:
                        StartHard();
                        break;

                    case 3:
                        StartEndGame();
                        break;
                }

                if (enemy != null)
                {
                    BattleStart();
                    if (DepthCur < DepthMax)
                        if(BattleAfter())
                            return;
                }

            } while (DepthCur <= DepthMax && !player.IsDead);

            if (player.IsDead)
                GameOver();

            else
                GameClear();
        }

        public void StartEasy()
        {
            DepthMax = 5;

            
            Console.Clear();

            switch (DepthCur)
            {
                default:
                    enemy = new MinionWolf(0);
                    break;
                case 3:
                    enemy = new EliteWolf(1);
                    break;
                case 4:
                    enemy = null;
                    break;
                case 5:
                    enemy = new ChampionWolf(2);
                    break;
            }
        }

        public void StartNormal()
        {
            DepthMax = 12;

            switch (DepthCur)
            {
                default:
                    enemy = new MinionWolf(1);
                    break;
                case 4:
                case 10:
                    enemy = new EliteWolf(2);
                    break;
                case 5:
                case 11:
                    enemy = null;
                    break;
                case 12:
                    enemy = new ChampionWolf(3);
                    break;
            }
        }

        public void StartHard()
        {
            DepthMax = 32;

            switch (DepthCur)
            {
                default:
                    enemy = new MinionWolf(2);
                    break;
                case 4:
                case 10:
                case 16:
                case 23:
                case 30:
                    enemy = new EliteWolf(3);
                    break;
                case 5:
                case 11:
                case 17:
                case 24:
                case 31:
                    enemy = null;
                    break;
                case 32:
                    enemy = new ChampionWolf(4);
                    break;
            }
        }

        public void StartEndGame()
        {
            DepthMax = 50;

            switch (DepthCur)
            {
                default:
                    enemy = new MinionWolf(3);
                    break;
                case 4:
                case 10:
                case 16:
                case 23:
                case 27:
                case 30:
                case 35:
                case 39:
                case 41:
                case 42:
                case 43:
                case 47:
                    enemy = new EliteWolf(4);
                    break;
                case 5:
                case 11:
                case 17:
                case 24:
                case 31:
                case 40:
                case 44:
                case 49:
                    enemy = null;
                    break;
                case 45:
                case 48:
                    enemy = new ChampionWolf(5);
                    break;
                case 50:
                    enemy = new DarkMoonWolf(6);
                    break;
            }
        }

        public void BattleStart()
        {
            Thread inputThread = new Thread(() => BattleInput());
            inputThread.Start();

            logList.Clear();
            player.UpdateAttribute();
            player.UpdateHp();
            enemy.UpdateAttribute();
            enemy.UpdateHp();

            BattleSequence();

            actionList.Clear();
            
            (player as IPlayer).Weapon.Attack.TurnCounterCurrent = 0;
            (player as IPlayer).Weapon.Skill.SkillCounterCurrent = 0;
            player.EffectList.Clear();
            player.UpdateAttribute();

            if (enemy.IsDead)
            {
                GameManager.Instance().ember += (int)((enemy as IMonster).EmberDrop * MathF.Pow(1.15f, enemy.Level));
                GameManager.Instance().amber += (int)((enemy as IMonster).AmberDrop * MathF.Pow(1.15f, enemy.Level));
                (player as IPlayer).GetExp((int)((enemy as IMonster).ExpDrop * MathF.Pow(1.15f, enemy.Level)));

                for(int i = 0; i < (enemy as IMonster).ItemDropList.Count; i++)
                    if ((enemy as IMonster).ItemDropList[i].CheckDrop())
                        GameManager.Instance().storage.GetItem((enemy as IMonster).ItemDropList[i].item);
            }
        }

        public void BattleSequence()
        {
            string tempString = string.Format($"{enemy.Name}이(가) 출몰하였다!");
            logList.Add(tempString);
            logList.Add("");
            Console.WriteLine(tempString);

            DisplayPlayerUI();

            actionList.Add(new ActionBlock((player as IPlayer).Weapon.Attack as IAction, player, enemy));
            actionList.Add(new ActionBlock((enemy as IMonster).Attack as IAction, enemy, player));
            Thread.Sleep(800);

            while (!enemy.IsDead && !player.IsDead)
            {
                ActionBlock tempBlock = actionList[0];
                actionList.RemoveAt(0);

                ResultBlock tempResultBlock = tempBlock.action.UseAction(tempBlock.user, tempBlock.target);
                
                CreateBattleLog(tempBlock, tempResultBlock);
                DisplayLog();
                DisplayPlayerUI();

                if (tempBlock.action.ActionType == 0)
                {
                    if (ReferenceEquals(tempBlock.user, player))
                    {
                        player.UpdateAttribute();

                        int j = 0;
                        while (j < player.EffectList.Count)
                        {
                            bool isOver = player.EffectList[j].ActivateEffect(player);

                            if (isOver)
                                player.EffectList.RemoveAt(j);
                            else
                                j++;
                        }

                        if (((player as IPlayer).Weapon.Skill as IAction).AttemptAction(player))
                            actionList.Add(new ActionBlock((player as IPlayer).Weapon.Skill as IAction, player, enemy));

                        actionList.Add(new ActionBlock((player as IPlayer).Weapon.Attack as IAction, player, enemy));
                    }

                    else
                    {
                        enemy.UpdateAttribute();

                        int j = 0;
                        while (j < enemy.EffectList.Count)
                        {
                            bool isOver = enemy.EffectList[j].ActivateEffect(enemy);

                            if (isOver)
                                enemy.EffectList.RemoveAt(j);
                            else
                                j++;
                        }

                        for (int i = 0; i < (enemy as IMonster).SkillList.Count; i++)
                            if (((enemy as IMonster).SkillList[i] as IAction).AttemptAction(enemy))
                                actionList.Add(new ActionBlock((enemy as IMonster).SkillList[i] as IAction, enemy, player));

                        actionList.Add(new ActionBlock((enemy as IMonster).Attack as IAction, enemy, player));
                    }
                }

                Thread.Sleep(800);
            }
        }

        public void BattleInput()
        {
            ConsoleKeyInfo input;

            while (!enemy.IsDead && !player.IsDead)
            {
                input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Z:
                        if ((player as IPlayer).ConsumableList[0] != null && ((player as IPlayer).ConsumableList[0].consumable as IAction).AttemptAction(player))
                        {
                            actionList.Add(new ActionBlock((player as IPlayer).ConsumableList[0].consumable as IAction, player, enemy));
                            (player as IPlayer).UseItem(0);
                        }
                        break;

                    case ConsoleKey.X:
                        if ((player as IPlayer).ConsumableList[1] != null && ((player as IPlayer).ConsumableList[1].consumable as IAction).AttemptAction(player))
                        {
                            actionList.Add(new ActionBlock((player as IPlayer).ConsumableList[1].consumable as IAction, player, enemy));
                            (player as IPlayer).UseItem(1);
                        }
                        break;

                    case ConsoleKey.C:
                        if ((player as IPlayer).ConsumableList[2] != null && ((player as IPlayer).ConsumableList[2].consumable as IAction).AttemptAction(player))
                        {
                            actionList.Add(new ActionBlock((player as IPlayer).ConsumableList[2].consumable as IAction, player, enemy));
                            (player as IPlayer).UseItem(2);
                        }
                        break;

                    case ConsoleKey.V:
                        if (((player as IPlayer).Dogma.Magic as IAction).AttemptAction(player))
                        {
                            GameManager.Instance().ember -= (player as IPlayer).Dogma.Magic.EmberCost;
                            actionList.Add(new ActionBlock((player as IPlayer).Dogma.Magic as IAction, player, enemy));
                        }
                        break;
                }
            }
        }

        public bool BattleAfter()
        {
            int optionCur = 0;
            int screenCondition = 0;
            bool result = false;

            string[] menuList = { "휴식", "진행" , "귀환"};

            while (screenCondition >= 0)
            {
                Console.Clear();
                DisplayPlayerUI();

                switch (screenCondition)
                {
                    case 0:
                        Console.WriteLine($"{enemy.Name}을(를) 처치하였다.");
                        Console.WriteLine($"어느 정도 여유가 있다. 무엇을 할까?");
                        Console.WriteLine();
                        break;
                    case 1:
                        Console.WriteLine("Ember를 소비하여 불을 지폈다.");
                        Console.WriteLine("잔잔한 불길에 마음이 차분해진다.");
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.WriteLine("Ember가 부족해서 불을 지필 수 없다.");
                        Console.WriteLine("다른 일을 생각 해 보자...");
                        Console.WriteLine();
                        break;
                }

                for (int i = 0; i < menuList.Length; i++)
                {
                    if (optionCur == i)
                        Console.Write("▶ ");
                    else
                        Console.Write("   ");

                    Console.Write(menuList[i]);

                    if (i == 0)
                        Console.WriteLine($"[{20 * DepthCur}Ember]");
                    else
                        Console.WriteLine();
                }

                ConsoleKeyInfo input;
                input = Console.ReadKey();

                // 화살표는 자동 진행.
                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        optionCur++;
                        if (optionCur >= menuList.Length)
                            optionCur = 0;
                        break;

                    case ConsoleKey.UpArrow:
                        optionCur--;
                        if (optionCur < 0)
                            optionCur = menuList.Length - 1;
                        break;

                    case ConsoleKey.Z:
                        switch (optionCur)
                        {
                            case 0:
                                if (GameManager.Instance().ember >= 20 * DepthCur)
                                {
                                    GameManager.Instance().ember -= 20 * DepthCur;
                                    player.TakeHeal((int)(player.HpMax * 0.35f));
                                    screenCondition = 1;
                                }
                                else
                                    screenCondition = 2;
                                break;
                            case 1:
                                screenCondition = -1;
                                break;
                            case 2:
                                Console.WriteLine("귀환하시겠습니까?   Z: O  K   X: Cancel");
                                bool tempBool = true;
                                while (tempBool)
                                {
                                    input = Console.ReadKey();

                                    switch (input.Key)
                                    {
                                        case ConsoleKey.Z:
                                            GameManager.Instance().SceneCurrent = GameManager.SceneNum.MainScene;
                                            result = true;
                                            screenCondition = -1;
                                            tempBool = false;
                                            break;

                                        case ConsoleKey.X:
                                            tempBool = false;
                                            break;
                                    }
                                }
                                break;
                        }
                        break;

                }
            }

            return result;
        }

        public void Rest()
        {
            Console.WriteLine("이미 불이 지펴져 있는 공간을 찾았다.");
            Console.WriteLine("나 말고 공략하는 사람이 있는 것일까.");
            Console.WriteLine("그건 아니겠지. 나 밖에 없는 걸.");
            Console.WriteLine();
            Console.WriteLine("... 차분한 불길에 마음이 차분해진다.");
            Console.WriteLine();
            if (player.HpCur < player.HpMax)
                Console.WriteLine($"[HP를 {player.HpMax - player.HpCur}만큼 회복하였다.]");
            else
                Console.WriteLine();
            Console.WriteLine("Press Any Button to Progress Dungeon.");

            player.UpdateHp();

            ConsoleKeyInfo input;
            input = Console.ReadKey();
        }

        public void CreateBattleLog(ActionBlock tempBlock, ResultBlock tempResultBlock)
        {
            int tempTop = Console.CursorTop;
            string tempLog;
            List<string> tempStringList = new List<string>();

            // 발동 부분
            tempStringList.Add($"{tempBlock.user.Name}은(는) ");

            if (tempResultBlock.ember < 0)
                tempStringList.Add($"{-tempResultBlock.ember}Ember를 소비하여 ");

            tempStringList.Add($"{tempBlock.action.Name}을(를) 사용했다!");

            // 피해 판정. 피해량이 양수면 데미지, 음수면 회복.
            if (tempResultBlock.damage > 0)
            {
                tempBlock.target.TakeDamage(tempResultBlock.damage, tempBlock.action.ActionType);
                tempStringList.Add($" 피해 {tempResultBlock.damage}!");
            }
            else if (tempResultBlock.damage < 0)
            {
                tempBlock.target.TakeHeal(-tempResultBlock.damage);
                tempStringList.Add($" 치유 {-tempResultBlock.damage}!");
            }

            // 회복 판정. 회복량이 양수면 회복, 음수면 데미지.
            if (tempResultBlock.heal < 0)
            {
                tempBlock.user.TakeDamage(-tempResultBlock.heal, tempBlock.action.ActionType);
                tempStringList.Add($" 자해 {-tempResultBlock.heal}!");
            }
            else if (tempResultBlock.heal > 0)
            {
                tempBlock.user.TakeHeal(tempResultBlock.heal);
                tempStringList.Add($" 회복 {tempResultBlock.heal}!");
            }
            if(tempStringList.Count > 0)
                logList.Add(string.Concat(tempStringList));
            tempStringList.Clear();

            if (tempResultBlock.buffList != null)
            {
                tempStringList.Add($"{tempBlock.user.Name}:");

                for (int i = 0; i < tempResultBlock.buffList.Count; i++)
                {
                    tempBlock.user.TakeEffect(tempResultBlock.buffList[i]);
                    tempStringList.Add($" [{tempResultBlock.buffList[i].Name}] ");
                }
                tempStringList.Add($"효과를 받았다.");
            }
            if (tempStringList.Count > 0)
                logList.Add(string.Concat(tempStringList));
            tempStringList.Clear();

            if (tempResultBlock.badStatusList != null)
            {
                tempStringList.Add($"{tempBlock.target.Name}:");

                for (int i = 0; i < tempResultBlock.badStatusList.Count; i++)
                {
                    tempBlock.user.TakeEffect(tempResultBlock.badStatusList[i]);
                    tempStringList.Add($" [{tempResultBlock.badStatusList[i].Name}] ");
                }
                tempStringList.Add($"효과를 받았다.");
            }
            if (tempStringList.Count > 0)
                logList.Add(string.Concat(tempStringList));
            tempStringList.Clear();

            if (tempResultBlock.ember > 0)
            {
                GameManager.Instance().ember += tempResultBlock.ember;
                tempStringList.Add($"{tempResultBlock.ember}Ember를 습득했다.");
            }
            if (tempStringList.Count > 0)
                logList.Add(string.Concat(tempStringList));
            tempStringList.Clear();

            logList.Add("");
        }

        public void DisplayLog()
        {
            Console.Clear();

            // 배틀 Log 출력
            for (int i = (int)MathF.Max(0, logList.Count - Console.WindowHeight + 10); i < logList.Count; i++)
                Console.WriteLine(logList[i]);
        }
        public void DisplayPlayerUI()
        { 
            // 플레어 UI 출력
            Console.SetCursorPosition(0, Console.WindowHeight - 8);

            if (enemy != null && !enemy.IsDead)
            {
                Console.Write($"Enemy: {enemy.Name}");

                for (int i = 0; i < enemy.EffectList.Count; i++)
                    Console.Write($"  [{enemy.EffectList[i].statusEffect.Name}]");
            }

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            switch (GameManager.Instance().gradeDifficulty)
            {
                case 0:
                    Console.Write($" 수사 |  ");
                    break;
                case 1:
                    Console.Write($" 조사 |  ");
                    break;
                case 2:
                    Console.Write($" 탐사 |  ");
                    break;
                case 3:
                    Console.Write($" 서사 |  ");
                    break;
            }

            Console.SetCursorPosition(8, Console.CursorTop);
            Console.Write($"{GameManager.Instance().player.Name} HP: {GameManager.Instance().player.HpCur} / {GameManager.Instance().player.HpMax}");
            
            Console.SetCursorPosition(49, Console.CursorTop);
            Console.WriteLine("Ember: {0,4}", GameManager.Instance().ember);

            Console.Write(" {0,2}층 |  ", DepthCur);

            Console.SetCursorPosition(8, Console.CursorTop);
            for (int i = 0; i < player.EffectList.Count; i++)
                Console.Write($"[{player.EffectList[i].statusEffect.Name}]  ");
            Console.WriteLine();

            Console.WriteLine("------------------------------------------------------------");

            if((player as IPlayer).ConsumableList[0] != null)
                Console.Write($"Z: {((player as IPlayer).ConsumableList[0].consumable as IItem).Name}({(player as IPlayer).ConsumableList[0].amount})                 ");
            else
                Console.Write($"Z: -----------------------");

            Console.SetCursorPosition(30, Console.CursorTop);
            if ((player as IPlayer).ConsumableList[1] != null)
                Console.Write($"X: {((player as IPlayer).ConsumableList[1].consumable as IItem).Name}({(player as IPlayer).ConsumableList[1].amount})                ");
            else
                Console.Write($"X: -----------------------");

            Console.WriteLine();

            if ((player as IPlayer).ConsumableList[2] != null)
                Console.Write($"C: {((player as IPlayer).ConsumableList[2].consumable as IItem).Name}({(player as IPlayer).ConsumableList[2].amount})                 ");
            else
                Console.Write($"C: -----------------------");

            Console.SetCursorPosition(30, Console.CursorTop);
            if ((player as IPlayer).Dogma != null && (player as IPlayer).Dogma.Magic != null)
                Console.Write($"V: {((player as IPlayer).Dogma.Magic as IAction).Name}[{(player as IPlayer).Dogma.Magic.EmberCost}Ember]                 ");
            else
                Console.Write($"V: -----------------------");

            Console.SetCursorPosition(0, 0);
        }

        public void GameClear()
        {
            Console.Clear();
            DisplayPlayerUI();

            GameManager.Instance().isCleared[GameManager.Instance().gradeDifficulty] = true;

            switch (GameManager.Instance().gradeDifficulty)
            {
                case 0:
                    Console.WriteLine("오늘도 희망이 한 꺼풀 늘어났다.");
                    Console.WriteLine("약소하지만 확실한 한 걸음.");
                    Console.WriteLine("이걸 쌓아서 태산을 이루는 거야.");
                    Console.WriteLine();
                    Console.WriteLine("... 아, 돌아가면 밥 해야되겠네.");
                    Console.WriteLine();
                    Console.WriteLine("Press Any Button to Return to Base");
                    GameManager.Instance().SceneCurrent = GameManager.SceneNum.MainScene;
                    break;
                case 1:
                    Console.WriteLine("언젠가 상인이 말 한 적이 있다..");
                    Console.WriteLine("자신이 스러지면 가계 물건들으로 공략하라고.");
                    Console.WriteLine("그 때가 되면 돈이 무엇이 중하겠느냐고.");
                    Console.WriteLine();
                    Console.WriteLine("... 근데 지금 자기 이름을 잊었는데 돈은 챙긴다니까.");
                    Console.WriteLine();
                    Console.WriteLine("Press Any Button to Return to Base");
                    GameManager.Instance().SceneCurrent = GameManager.SceneNum.MainScene;
                    break;
                case 2:
                    Console.WriteLine("스러진다는 건 죽는 걸 의미하는 게 아니다.");
                    Console.WriteLine("자아가 희미해져서 자신 조자 잊어버리는 걸 말한다.");
                    Console.WriteLine("지금 옆에서 전리품을 수거해주시는 형님들도 그러하다.");
                    Console.WriteLine("이 얼마나 멋진 직업정신인가.");
                    Console.WriteLine();
                    Console.WriteLine("... 스러지기 전에는 빼돌리기도 했었는데, 지금도 그러진 않겠지?");
                    Console.WriteLine();
                    Console.WriteLine("Press Any Button to Return to Base");
                    GameManager.Instance().SceneCurrent = GameManager.SceneNum.MainScene;
                    break;
                case 3:
                    Console.WriteLine("드디어 끝났다.");
                    Console.WriteLine("쓰라렸던 여정도, 절망적인 반복도.");
                    Console.WriteLine("이제 다시 돌아가자.");
                    Console.WriteLine();
                    Console.WriteLine("... 그리고, 별빛을 찾아보는 거야.");
                    Console.WriteLine();
                    Console.WriteLine("Press Any Button to Return to Title");
                    GameManager.Instance().SceneCurrent = GameManager.SceneNum.TitleScene;
                    break;
        }

            ConsoleKeyInfo input;
            input = Console.ReadKey();
        }

        public void GameOver()
        {
            Console.Clear();
            DisplayPlayerUI();

            Console.WriteLine("... 하늘은 정말 더럽게 맑구나.");
            Console.WriteLine("희망찼던 반복도, 보람찼던 여정도,");
            Console.WriteLine("그저 되풀이될 뿐.");
            Console.WriteLine("결국 나 조차도 스러져 가기에 반복하는 거겠지.");
            Console.WriteLine();
            Console.WriteLine("식당 아주머니, 아주머니가 해주신 밥이 그리워요...");
            Console.WriteLine(); 
            Console.WriteLine("Press Any Button to Return to the Base");

            GameManager.Instance().SceneCurrent = GameManager.SceneNum.MainScene;

            ConsoleKeyInfo input;
            input = Console.ReadKey();
        }
    }
}
