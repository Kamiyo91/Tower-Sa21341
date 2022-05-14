using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs
{
    public class BattleUnitBuf_Remembrance_Sa21341 : BattleUnitBuf
    {
        private Random _random;

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
            if (_random.Next(0, 100) >= 25 + stack) return;
            var targetBuffs = behavior.card.target.bufListDetail.GetActivatedBufList()
                .Where(x => x.positiveType == BufPositiveType.Positive).ToList();
            if (!targetBuffs.Any()) return;
            var targetBuff = RandomUtil.SelectOne(targetBuffs);
            if (targetBuff.stack > 1) targetBuff.stack = 1;
            _owner.bufListDetail.AddBuf(targetBuff);
        }
    }
}