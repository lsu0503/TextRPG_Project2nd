using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.System;
using TextRPG_Project2nd.Dogma;
using TextRPG_Project2nd.Item.Cloth;
using TextRPG_Project2nd.Item.Weapon;
using TextRPG_Project2nd.Item.Consumables;
using TextRPG_Project2nd.Item;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.Character.Monster;

namespace TextRPG_Project2nd
{
    internal class GameManager
    {
        static private GameManager instance;

        public enum SceneNum
        {
            TitleScene,
            LoadScene,
            CreateScene,
            MainScene,
            DungeonScene
        }
        public SceneNum SceneCurrent;

        public List<DogmaIndex> dogmaList = new List<DogmaIndex>();

        public int saveSlot;
        public Player player;
        public int ember;
        public int amber;

        public int gradeDifficulty = 0;
        public bool[] isCleared = new bool[4];

        public Shop shop = new Shop();
        public Storage storage = new Storage();

        public int bossNum;

        public GameManager()
        {
            amber = 1000;

            ICloth tempCloth = new WondererCloth();
            storage.GetItem((IItem)tempCloth);

            IWeapon tempWeapon = new BluntRod();
            storage.GetItem((IItem)tempWeapon);

            player = new Player(tempCloth, tempWeapon);

            gradeDifficulty = 0;
            isCleared = new bool[] { false, false, false, false };

            dogmaList.Add(new DogmaIndex(new DogmaNatalana(), true));
            dogmaList.Add(new DogmaIndex(new DogmaErzebeta(), true));
        }

        public static GameManager Instance()
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }

        public void GameEnd()
        {
            Environment.Exit(0);
        }
    }
}

