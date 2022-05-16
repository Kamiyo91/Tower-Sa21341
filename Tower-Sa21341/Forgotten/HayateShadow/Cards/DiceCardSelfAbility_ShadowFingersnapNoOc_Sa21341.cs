﻿namespace VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Cards
{
    public class DiceCardSelfAbility_ShadowFingersnapNoOc_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnApplyCard()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Aim);
        }

        public override void OnReleaseCard()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }
    }
}