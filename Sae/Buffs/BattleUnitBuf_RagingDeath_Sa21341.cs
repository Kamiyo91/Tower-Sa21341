namespace VortexTower.Sae.Buffs
{
    public class BattleUnitBuf_RagingDeath_Sa21341 : BattleUnitBuf
    {
        public override void OnRoundStartAfter()
        {
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, _owner);
            _owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, _owner);
        }
    }
}