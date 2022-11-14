using LOR_DiceSystem;

namespace VortexTower.Sae.Buffs
{
    public class BattleUnitBuf_AtkStance_Sa21341 : BattleUnitBuf
    {
        public BattleUnitBuf_AtkStance_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "AtkStance_Sa21341";
        protected override string keywordIconId => "AtkStance_Sa21341";

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
            if (_owner.hp > _owner.MaxHp * 0.25f) return;
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmg = 2,
                breakDmg = 2
            });
        }
    }
}