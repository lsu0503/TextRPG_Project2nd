using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Action.Magic;

namespace TextRPG_Project2nd.Dogma
{
    public class DogmaIndex
    {
        public IDogma dogma;
        public bool isUnlocked;

        public DogmaIndex(IDogma _dogma, bool _isUnlocked)
        {
            dogma = _dogma;
            isUnlocked = _isUnlocked;
        }
    }

    public interface IDogma
    {
        string Name { get; }
        string[] DetailDogma { get; }

        IMagic Magic { get; }
    }
}
