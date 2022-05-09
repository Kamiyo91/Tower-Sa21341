using System;
using System.Linq;

namespace VortexLabyrinth_Sa21341.Zero.Buffs
{
    public class BattleUnitBuf_BlueFlame_Sa21341 : BattleUnitBuf
    {
        private Random _random;
        protected override string keywordId => "BlueFlame_Sa21341";
        protected override string keywordIconId => "BlueFlame_Sa21341";

        public override void OnRoundEnd()
        {
            if (stack > 0) stack--;
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            _random = new Random();
        }

        public override void OnSuccessAttack(BattleDiceBehavior behavior)
        {
            if (_random.Next(0, 100) >= 25 + stack) return;
            var targetBuff = RandomUtil.SelectOne(behavior.card.target.bufListDetail.GetActivatedBufList()
                .Where(x => x.positiveType == BufPositiveType.Positive).ToList());
            if (targetBuff.stack < 2) behavior.card.target.bufListDetail.RemoveBuf(targetBuff);
            else
                targetBuff.stack--;
        }
    }
}