namespace VortexLabyrinth_Sa21341.BluePetal.Buffs
{
    public class BattleUnitBuf_BluePetal_Sa21341 : BattleUnitBuf
    {
        protected override string keywordId => "BluePetal_Sa21341";
        protected override string keywordIconId => "BluePetal_Sa21341";

        public override void OnRoundEnd()
        {
            stack--;
            if (stack == 0) Destroy();
        }

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