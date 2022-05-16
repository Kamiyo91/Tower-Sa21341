using System.Linq;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Cards
{
    public class DiceCardSelfAbility_ShadowBuff_Sa21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBufList()
                       .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341)?.stack > 24 &&
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
            unit.bufListDetail.AddBuf(new BattleUnitBuf_ShadowBuff_Sa21341());
            if (unit.bufListDetail.GetActivatedBufList()
                    .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341) is BattleUnitBuf_Remembrance_Sa21341
                buff)
                buff.AddStacks(-25);
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }
    }
}