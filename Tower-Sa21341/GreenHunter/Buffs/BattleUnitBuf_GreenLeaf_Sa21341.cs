namespace VortexLabyrinth_Sa21341.GreenHunter.Buffs
{
    public class BattleUnitBuf_GreenLeaf_Sa21341 : BattleUnitBuf
    {
        protected override string keywordId => "GreenLeaf_Sa21341";
        protected override string keywordIconId => "GreenLean_Sa21341";

        public override int GetDamageReductionRate()
        {
            return stack;
        }
    }
}