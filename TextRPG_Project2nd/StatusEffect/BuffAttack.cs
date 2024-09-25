using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Character;

namespace TextRPG_Project2nd.StatusEffect
{
    internal class BuffAttack : IStatusEffect
    {
        int statusType= 0;
        string name = "강화";
        int turnMax;
        int power;

        ICharacter target;

        public int StatusType { get { return statusType; } }
        public string Name { get { return name; } }
        public int TurnMax { get { return turnMax; } set { turnMax = value; } }
        public ICharacter Target { get { return target; } set { target = value; } }

        public BuffAttack(int _turn, int _power)
        {
            turnMax = _turn;
            power = _power;
        }

        public void ActivateEffect(ICharacter target)
        {
            target.BaseAtk = target.BaseAtk * (100 + power) / 100;
        }
    }
}
