using System.Linq;
using VortexTower.Forgotten.KamiyoShadow.Buffs;

namespace VortexTower.Forgotten.KamiyoShadow.Cards
{
    public class DiceCardSelfAbility_ShadowBuff_Sa21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBufList()
                       .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341)?.stack > 9 &&
                   !owner.cardSlotDetail.cardAry.Exists(x =>
                       x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 52));
        }

        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit)
        {
            if (!unit.bufListDetail.HasBuf<BattleUnitBuf_ShadowBuff_Sa21341>())
                unit.bufListDetail.AddBuf(new BattleUnitBuf_ShadowBuff_Sa21341());
            unit.bufListDetail.AddBuf(new BattleUnitBuf_0CardCostPlayer_Sa21341());
            if (unit.bufListDetail.GetActivatedBufList()
                    .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341) is BattleUnitBuf_Remembrance_Sa21341
                buff)
                buff.AddStacks(-10);
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }
    }
}