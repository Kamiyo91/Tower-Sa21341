﻿namespace VortexTower.Sae.Dice
{
    public class DiceCardAbility_RagingDeathDice_Sa21341 : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            if (owner.hp > owner.MaxHp * 0.25f) return;
            if (card?.target?.currentDiceAction?.cardBehaviorQueue.Count > 0)
                card?.target?.currentDiceAction?.DestroyDice(DiceMatch.AllDice);
        }
    }
}