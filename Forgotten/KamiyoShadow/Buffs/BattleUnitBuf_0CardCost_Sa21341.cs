using BigDLL4221.Utils;

namespace VortexTower.Forgotten.KamiyoShadow.Buffs
{
    public class BattleUnitBuf_0CardCost_Sa21341 : BattleUnitBuf
    {
        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return -99;
        }

        public override void OnRoundStart()
        {
            UnitUtil.DrawUntilX(_owner, 7);
        }
    }
}