﻿namespace VortexLabyrinth_Sa21341.Sae.Dices
{
    public class DiceCardAbility_StruggleDefDice_Sa21341 : DiceCardAbilityBase
    {
        public override void OnWinParryingDefense()
        {
            if (owner.hp > owner.MaxHp * 0.25f) return;
            owner.allyCardDetail.DrawCards(1);
        }
    }
}