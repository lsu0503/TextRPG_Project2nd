using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Action.Magic;

namespace TextRPG_Project2nd.Dogma
{
    public class DogmaErzebeta : IDogma
    {
        int dogmaID = 1;
        string name = "광혈의 마녀";
        string[] detailDogma = new string[] { "그녀는 한 때, 영웅이라 불리었다.",
                                              "마왕을 토벌하고 돌아오는 그녀는 완벽한 영웅이었다.",
                                              "... 마왕이 진짜로 있었다면 말이다."};

        public IMagic magic = new MagicBloodThons();

        public int DogmaID { get { return dogmaID; } }
        public string Name { get { return name; } }
        public string[] DetailDogma { get { return detailDogma; } }

        public IMagic Magic { get { return magic; } }
    }
}
