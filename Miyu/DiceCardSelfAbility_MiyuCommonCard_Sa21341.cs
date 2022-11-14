namespace VortexTower.Miyu
{
    public class DiceCardSelfAbility_MiyuCommonCard_Sa21341 : DiceCardSelfAbilityBase
    {
        public override string[] Keywords
        {
            get
            {
                return new[]
                {
                    "Healer_Sa21341"
                };
            }
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                dmgRate = -9999,
                breakRate = -9999
            });
        }

        public override bool IsValidTarget(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            return targetUnit != null && targetUnit.faction == unit.faction;
        }

        public override bool IsTargetChangable(BattleUnitModel attacker)
        {
            return false;
        }

        public override bool IsOnlyAllyUnit()
        {
            return true;
        }
    }
}