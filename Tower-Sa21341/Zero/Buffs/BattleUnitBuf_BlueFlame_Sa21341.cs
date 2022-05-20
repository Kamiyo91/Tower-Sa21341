using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace VortexLabyrinth_Sa21341.Zero.Buffs
{
    public class BattleUnitBuf_BlueFlame_Sa21341 : BattleUnitBuf
    {
        private Random _random;
        private int _count;
        protected override string keywordId => "BlueFlame_Sa21341";
        protected override string keywordIconId => "BlueFlame_Sa21341";

        public override void OnRoundEnd()
        {
            _count = 0;
            if (stack > 0) stack--;
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            _random = new Random();
        }

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            if (_count > 2) return;
            if (_random.Next(0, 100) >= 25 + stack) return;
            var targetBuffs = behavior.card.target.bufListDetail.GetActivatedBufList()
                .Where(x => x.positiveType == BufPositiveType.Positive).ToList();
            if (!targetBuffs.Any()) return;
            var targetBuff = RandomUtil.SelectOne(targetBuffs);
            if (targetBuff.stack < 2) behavior.card.target.bufListDetail.RemoveBuf(targetBuff);
            else
                targetBuff.stack--;
            _count++;
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

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (stack > 24) behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }
    }
}