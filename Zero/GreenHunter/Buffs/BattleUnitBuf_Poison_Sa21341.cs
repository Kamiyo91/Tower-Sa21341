using BigDLL4221.Buffs;

namespace VortexTower.Zero.GreenHunter.Buffs
{
    public class BattleUnitBuf_Poison_Sa21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        protected override string keywordId => "Poison_Sa21341";
        protected override string keywordIconId => "Poison_Sa21341";
        public override BufPositiveType positiveType => BufPositiveType.Negative;
        public override int MaxStack => 10;
        public override int AdderStackEachScene => -2;
        public override bool DestroyedAt0Stack => true;

        public override void OnRoundEnd()
        {
            _owner.TakeDamage(stack);
            base.OnRoundEnd();
        }

        public override int GetDamageIncreaseRate()
        {
            return stack;
        }
    }
}