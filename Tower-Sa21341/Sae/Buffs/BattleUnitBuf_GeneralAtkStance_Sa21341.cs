using LOR_DiceSystem;

namespace VortexLabyrinth_Sa21341.Sae.Buffs
{
    public class BattleUnitBuf_GeneralAtkStance_Sa21341 : BattleUnitBuf
    {
        public BattleUnitBuf_GeneralAtkStance_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "GeneralAtkStance_Sa21341";
        protected override string keywordIconId => "GeneralAtkStance_Sa21341";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (behavior.Detail == BehaviourDetail.Slash || behavior.Detail == BehaviourDetail.Penetrate ||
                behavior.Detail == BehaviourDetail.Hit)
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        power = 1
                    });
        }

        public override void BeforeGiveDamage(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmgRate = 10,
                breakRate = 10
            });
        }
    }
}