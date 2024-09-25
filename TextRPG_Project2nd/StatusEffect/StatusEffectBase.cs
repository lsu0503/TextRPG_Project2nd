using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Character;

namespace TextRPG_Project2nd.StatusEffect
{
    public class StatusEffectSlot
    {
        public IStatusEffect statusEffect;
        public int turnRemain;

        public StatusEffectSlot(IStatusEffect _statusEffect)
        {
            statusEffect = _statusEffect;
            turnRemain = statusEffect.TurnMax;
        }

        public bool ActivateEffect(ICharacter target)
        {
            statusEffect.ActivateEffect(target);
            turnRemain--;

            if (turnRemain < 0)
                return true;
            else
                return false;
        }
    }

    public interface IStatusEffect
    {
        int StatusType { get; }
        string Name { get; }
        int TurnMax { get; set; }
        ICharacter Target { get; set; }

        public void ActivateEffect(ICharacter target);
    }
}
