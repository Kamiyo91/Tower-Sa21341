using BigDLL4221.Buffs;
using BigDLL4221.Utils;

namespace VortexTower.Sae.Buffs
{
    public class BattleUnitBuf_SaeSecondPhase_Sa21341 : BattleUnitBuf_BaseBufChanged_DLL4221
    {
        public BattleUnitBuf_SaeSecondPhase_Sa21341() : base(infinite: true, lastOneScene: false)
        {
        }

        protected override string keywordId => "SuddenDeath_Sa21341";
        protected override string keywordIconId => "SuddenDeath_Sa21341";
        public override int MaxStack => 40;

        public override void OnRoundStartAfter()
        {
            if (stack > 39) UnitUtil.VipDeath(_owner);
            OnAddBuf(-999);
        }

        public override void BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            OnAddBuf(dmg);
        }
    }
}