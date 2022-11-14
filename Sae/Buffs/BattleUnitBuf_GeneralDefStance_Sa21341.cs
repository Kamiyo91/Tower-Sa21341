using LOR_DiceSystem;

namespace VortexTower.Sae.Buffs
{
    public class BattleUnitBuf_GeneralDefStance_Sa21341 : BattleUnitBuf
    {
        public BattleUnitBuf_GeneralDefStance_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "GeneralDefStance_Sa21341";
        protected override string keywordIconId => "GeneralDefStance_Sa21341";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Evasion || behavior.Detail == BehaviourDetail.Guard)
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        power = 1
                    });
        }

        public override bool IsImmune(BufPositiveType posType)
        {
            return posType == BufPositiveType.Negative;
        }

        public override void OnRoundEndTheLast()
        {
            _owner.RecoverHP(4);
        }
    }
}