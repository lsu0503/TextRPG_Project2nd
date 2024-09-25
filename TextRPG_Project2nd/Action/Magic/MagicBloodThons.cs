using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Character;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Action.Magic
{
    internal class MagicBloodThons: IAction, IMagic
    {
        // IAction 구성요소
        int actionType= 2;
        string name = "피 무리 가시";
        string[] detailAction = {"수많은 피의 가시를 발사한다.",
                                "피의 가시는 출혈을 유발한다.",
                                "",
                                "마녀의 힘은 본디 생명이었다.",
                                "그것이 삶을 의미하지는 않았지만 말이다." };
        int power = 10;

        List<EffectAtk> effectAtkList = new List<EffectAtk>() {new EffectAtk(new StatusBleeding(7, 3), 0.4f), 
                                                               new EffectAtk(new StatusBleeding(7, 3), 0.4f),
                                                               new EffectAtk(new StatusBleeding(7, 3), 0.4f) };
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

            List<IStatusEffect> tempEffectList = new List<IStatusEffect>();
            for(int i =0; i < EffectAtkList.Count; i++)
            {
                if (new Random().NextDouble() < EffectAtkList[i].accuracy)
                    tempEffectList.Add(effectAtkList[i].effect);
            }

            return new ResultBlock(damage, 0, null, tempEffectList, -emberCost);
        }

        // IMagic 구성요소
        int emberCost = 85;

        public int EmberCost { get { return emberCost; } }
    }
}
