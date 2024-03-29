﻿using BigDLL4221.Utils;

namespace VortexTower.Forgotten.KamiyoShadow.Passives
{
    public class PassiveAbility_ForgottenMask_Sa21341 : PassiveAbilityBase
    {
        public override void OnStartTargetedOneSide(BattlePlayingCardDataInUnitModel attackerCard)
        {
            UnitUtil.SetPassiveCombatLog(this, owner);
            attackerCard?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
            });
        }

        public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
        {
            BattlePlayingCardDataInUnitModel battlePlayingCardDataInUnitModel;
            if (card == null)
            {
                battlePlayingCardDataInUnitModel = null;
            }
            else
            {
                var target = card.target;
                battlePlayingCardDataInUnitModel = target?.currentDiceAction;
            }

            var battlePlayingCardDataInUnitModel2 = battlePlayingCardDataInUnitModel;
            UnitUtil.SetPassiveCombatLog(this, owner);
            battlePlayingCardDataInUnitModel2?.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                max = -1
            });
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1, max = 1 });
        }
    }
}