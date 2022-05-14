﻿using LOR_DiceSystem;
using VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Cards
{
    public class DiceCardSelfAbility_FerventCut_Sa21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 1;
        private int _atkLand;

        public override void OnUseCard()
        {
            owner.cardSlotDetail.RecoverPlayPoint(1);
            _atkLand = 0;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (behavior.Type == BehaviourType.Atk) _atkLand++;
        }

        public override void OnEndBattle()
        {
            if (_atkLand < Check) return;
            foreach (var battleDiceCardModel in owner.allyCardDetail.GetAllDeck()
                         .FindAll(x => x != card.card && x.GetID() == card.card.GetID()))
            {
                battleDiceCardModel.GetBufList();
                battleDiceCardModel.AddCost(-1);
            }

            var buf =
                owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_ShadowEntertainMe_Sa21341) as
                    BattleUnitBuf_ShadowEntertainMe_Sa21341;
            buf?.AddStacks(1);
        }
    }
}