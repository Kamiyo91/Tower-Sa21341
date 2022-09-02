using UnityEngine;

namespace VortexLabyrinth_Sa21341.GreenHunter.Buffs
{
    public class BattleUnitBuf_Poison_Sa21341 : BattleUnitBuf
    {
        protected override string keywordId => "Poison_Sa21341";
        protected override string keywordIconId => "Poison_Sa21341";
        public override BufPositiveType positiveType => BufPositiveType.Negative;

        public override void OnRoundEnd()
        {
            _owner.TakeDamage(stack);
            if (stack - 2 > 0) stack -= 2;
            else _owner.bufListDetail.RemoveBuf(this);
        }

        public override void OnAddBuf(int addedStack)
        {
            stack += addedStack;
            stack = Mathf.Clamp(stack, 0, 10);
        }

        public override int GetDamageIncreaseRate()
        {
            return stack;
        }

    }
}