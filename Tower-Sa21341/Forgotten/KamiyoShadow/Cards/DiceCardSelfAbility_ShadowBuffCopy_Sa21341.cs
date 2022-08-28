using System;
using System.Linq;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Cards
{
    public class DiceCardSelfAbility_ShadowBuffCopy_Sa21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBufList()
                       .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341)?.stack > 2 &&
                   !owner.cardSlotDetail.cardAry.Exists(x =>
                       x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 52) || x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 69));
        }

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit, targetUnit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel owner, BattleUnitModel unit)
        {
            var buff = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341) as BattleUnitBuf_Remembrance_Sa21341;
            buff?.AddStacks(-3);
            var targetBuffs = unit.bufListDetail.GetActivatedBufList()
                .Where(x => x.positiveType == BufPositiveType.Positive).ToList();
            if (!targetBuffs.Any()) return;
            var targetBuffType = RandomUtil.SelectOne(targetBuffs).GetType();
            var buffPlus = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x.GetType() == targetBuffType);
            if (buffPlus == null)
            {
                var targetBuff = (BattleUnitBuf)Activator.CreateInstance(targetBuffType);
                targetBuff.stack = 1;
                owner.bufListDetail.AddBuf(targetBuff);
            }
            else
            {
                buffPlus.stack++;
            }
        }

        public override bool IsTargetableAllUnit()
        {
            return true;
        }

        public override bool IsValidTarget(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            return targetUnit.bufListDetail.GetActivatedBufList().Any(x => x.positiveType == BufPositiveType.Positive);
        }
    }
}