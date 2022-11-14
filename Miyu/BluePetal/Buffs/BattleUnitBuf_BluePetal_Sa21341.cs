using BigDLL4221.Buffs;

namespace VortexTower.Miyu.BluePetal.Buffs
{
    public class BattleUnitBuf_BluePetal_Sa21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        public BattleUnitBuf_BluePetal_Sa21341() : base(infinite: true, lastOneScene: false)
        {
        }

        protected override string keywordId => "BluePetal_Sa21341";
        protected override string keywordIconId => "BluePetal_Sa21341";
        public override BufPositiveType positiveType => BufPositiveType.Negative;
        public override int MaxStack => 15;
        public override int AdderStackEachScene => -1;
        public override bool DestroyedAt0Stack => true;

        public override bool CanRecoverHp(int amount)
        {
            return stack < 10;
        }

        public override bool CanRecoverBreak(int amount)
        {
            return stack < 10;
        }

        public override int GetDamageIncreaseRate()
        {
            return stack;
        }
    }
}