using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs
{
    public class BattleUnitBuf_Remembrance_Sa21341 : BattleUnitBuf
    {
        private Random _random;
        private int _count;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "Remembrance_Sa21341";
        protected override string keywordIconId => "Forgotten_Sa21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            _random = new Random();
        }

        public override int GetDamageIncreaseRate()
        {
            return stack;
        }

        public override void OnAddBuf(int addedStack)
        {
            stack += addedStack;
            stack = Mathf.Clamp(stack, 0, 25);
        }

        public void AddStacks(int stacks)
        {
            stack += stacks;
            stack = Mathf.Clamp(stack, 0, 25);
        }

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            if (_count > 2) return;
            if (_random.Next(0, 100) >= 25 + stack) return;
            var targetBuffs = behavior.card.target.bufListDetail.GetActivatedBufList()
                .Where(x => x.positiveType == BufPositiveType.Positive).ToList();
            if (!targetBuffs.Any()) return;
            var targetBuffType = RandomUtil.SelectOne(targetBuffs).GetType();
            var buffPlus = _owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x.GetType() == targetBuffType);
            if (buffPlus == null)
            {
                var targetBuff = (BattleUnitBuf)Activator.CreateInstance(targetBuffType);
                targetBuff.stack = 1;
                _owner.bufListDetail.AddBuf(targetBuff);
            }
            else
            {
                buffPlus.stack++;
            }
            _count++;
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            AddStacks(1);
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            AddStacks(-1);
        }

        public override void OnRoundEnd()
        {
            _count = 0;
            AddStacks(-1);
        }
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (stack > 24) behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }
    }
}