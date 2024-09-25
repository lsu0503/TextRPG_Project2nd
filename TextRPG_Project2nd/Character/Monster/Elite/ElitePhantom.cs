using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Project2nd.Action;
using TextRPG_Project2nd.Action.Skill;
using TextRPG_Project2nd.Item;
using TextRPG_Project2nd.Item.Consumables;
using TextRPG_Project2nd.Item.Weapon;
using TextRPG_Project2nd.StatusEffect;

namespace TextRPG_Project2nd.Character.Monster
{
    internal class ElitePhantom : ICharacter, IMonster
    {
        // ICharacter 구성 요소 [Level 제외]
        string name = "삿된 잔재";
        int hpMax;
        int hpCur;
        bool isDead = false;

        int baseAtk;
        int[] typeAtk;

        int[] defStatus;

        int level;

        int[] attribute;

        List<StatusEffectSlot> effectList = new List<StatusEffectSlot>();

        public string Name { get { return name; } set { name = value; } }
        public int HpMax { get { return hpMax; } set { hpMax = value; } }
        public int HpCur { get { return hpCur; } set { hpCur = value; } }
        public bool IsDead { get { return isDead; } set { isDead = value; } }

        public int BaseAtk { get { return baseAtk; } set { baseAtk = value; } }
        public int[] TypeAtk { get { return typeAtk; } set { typeAtk = value; } }

        public int[] DefStatus { get { return defStatus; } set { defStatus = value; } }

        public int Level { get { return level; } set { level = value; } }

        public int[] Attribute { get { return attribute; } set { attribute = value; } }

        public List<StatusEffectSlot> EffectList { get { return effectList; } set { effectList = value; } }

        public int TakeDamage(int damage, int type)
        {
            int calcDamage = damage * ((100 - defStatus[type]) / (100 + attribute[0]));

            hpCur -= damage;
            if (hpCur < 0)
            {
                hpCur = 0;
                isDead = true;
            }

            return calcDamage;
        }

        public int TakeHeal(int heal)
        {
            int calcHeal = (int)MathF.Min(heal, hpMax - hpCur);
            hpCur += calcHeal;
            return calcHeal;
        }

        public void TakeEffect(IStatusEffect _effect)
        {
            effectList.Add(new StatusEffectSlot(_effect));
        }

        public void ActivateEffect()
        {
            bool isOver;
            int i = 0;

            while (i < effectList.Count)
            {
                isOver = effectList[i].ActivateEffect(this);

                if (isOver)
                    effectList.RemoveAt(i);

                else
                    i++;
            }
        }

        public void UpdateAttribute()
        {
            attribute = originAttribute.ToArray();
            defStatus = originDefStatus.ToArray();
            typeAtk = originTypeAtk.ToArray();

            baseAtk = (int)(originAtk * (MathF.Pow(1.25f, level)));
        }

        public void UpdateHp()
        {
            hpMax = (int)(originHp * (MathF.Pow(1.25f, level)));
            hpCur = hpMax;
        }

        // IMonster 구성 요소
        int originHp = 150;
        int originAtk = 10;

        int[] originTypeAtk = { 65, 135, 100 };
        int[] originDefStatus = { 5, 5, 20 };
        int[] originAttribute = { 100, 100, 100 };

        int emberDrop = 55;
        int amberDrop = 55;
        List<ItemDrop> itemDropList = new List<ItemDrop>() { new ItemDrop(new SoulStoneFragment(), 0.35f) };
        int expDrop = 55;

        IAttack attack = new AttackBase("공격", 10, null, 1);
        List<ISkill> skillList = new List<ISkill>() { new SkillBash() };

        public int OriginHp { get { return originHp; } }
        public int OriginAtk { get { return originAtk; } }

        public int[] OriginTypeAtk { get { return originTypeAtk; } }
        public int[] OriginDefStatus { get{ return originDefStatus; } }
        public int[] OriginAttribute { get{ return originAttribute; } }

        public int EmberDrop { get { return emberDrop; } }
        public int AmberDrop { get { return amberDrop; } }
        public List<ItemDrop> ItemDropList { get { return itemDropList; } }
        public int ExpDrop { get { return expDrop; } }

        public IAttack Attack { get { return attack; } }
        public List<ISkill> SkillList { get { return skillList; } }

        public ElitePhantom(int _level)
        {
            level = _level;
            isDead = false;

            UpdateAttribute();
            UpdateHp();
        }
    }
}
