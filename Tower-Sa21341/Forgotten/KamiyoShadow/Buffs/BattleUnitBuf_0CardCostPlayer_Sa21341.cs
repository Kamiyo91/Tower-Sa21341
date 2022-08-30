using KamiyoStaticUtil.Utils;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs
{
    public class BattleUnitBuf_0CardCostPlayer_Sa21341 : BattleUnitBuf
    {
        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return -99;
        }
        public override void OnRoundEnd()
        {
            _owner.bufListDetail.RemoveBuf(this);
        }
        public void AddBuff()
        {
            var solo = UnitUtil.SupportCharCheck(_owner) != 1 ? 1 : 3;
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, solo);
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, solo);
        }
        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            AddBuff();
        }
    }
}
