using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Action.Magic;

namespace TextRPG_Project2nd.Dogma
{
    public class DogmaNatalana : IDogma
    {
        string name = "성광의 나탈라나";
        string[] detailDogma = new string[] { "혹자가 말하길, \"그녀는 별빛을 품고 있었다.\"",
                                              "혹자가 말하길, \"별빛의 아름다움은 그녀를 위한 것.\"",
                                              "그렇기에 그녀가 걸었던 길은, 더할나위 없는 모독이었다."};

        public IMagic magic = new MagicFallingStella();

        public string Name { get { return name; } }
        public string[] DetailDogma { get { return detailDogma; } }

        public IMagic Magic { get { return magic; } }
    }
}
