﻿using System.Linq;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Cards
{
    public class DiceCardSelfAbility_ShadowBuffCopy_Sa21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBufList()
                       .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341)?.stack > 4 &&
                   !owner.cardSlotDetail.cardAry.Exists(x =>
                       x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 33));
        }

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit, targetUnit, card.card.GetID());
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel owner, BattleUnitModel unit, LorId cardId)
        {
            var buff = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341) as BattleUnitBuf_Remembrance_Sa21341;
            buff?.AddStacks(-5);
            var targetBuffs = unit.bufListDetail.GetActivatedBufList()
                .Where(x => x.positiveType == BufPositiveType.Positive).ToList();
            if (!targetBuffs.Any())
            {
                owner.personalEgoDetail.AddCard(cardId);
                buff?.AddStacks(+5);
                return;
            }

            var targetBuff = RandomUtil.SelectOne(targetBuffs);
            if (targetBuff.stack > 1) targetBuff.stack = 1;
            owner.bufListDetail.AddBuf(targetBuff);
        }
    }
}