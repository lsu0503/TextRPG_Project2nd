using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Action.Magic
{
    internal class MagicFallingStella: IAction, IMagic
    {
        // IAction 구성요소
        int actionType= 2;
        string name = "내리는 별빛";
        string[] detailAction = {"하늘에서 별빛이 내린다.",
                                 "아름다운, 그러면서도 순수한 파괴.",
                                 "그것은 알량한 신성 모독이었다." };
        int power = 50;

        List<EffectAtk> effectAtkList = new List<EffectAtk>();
        List<IStatusEffect> effectList = new List<IStatusEffect>();

        public int ActionType { get { return actionType; } }
        public string Name { get { return name; } }
        public string[] DetailAction { get { return detailAction; } }
        public int Power { get { return power; } }

        public List<EffectAtk> EffectAtkList { get { return effectAtkList; } }
        public List<IStatusEffect> EffectList { get { return effectList; } }

        public bool AttemptAction(ICharacter user)
        {
            if (GameManager.Instance().ember > EmberCost)
                return true;
            else
                return false;
        }

        public ResultBlock UseAction(ICharacter user, ICharacter target)
        {
            int damage = (int)(power * user.BaseAtk / 100.0f * user.TypeAtk[2] / 100.0f * user.Attribute[2] / 100.0f);

            return new ResultBlock(damage, 0, null, null, -emberCost);
        }

        // IMagic 구성요소
        int emberCost = 50;

        public int EmberCost { get { return emberCost; } }
    }
}
