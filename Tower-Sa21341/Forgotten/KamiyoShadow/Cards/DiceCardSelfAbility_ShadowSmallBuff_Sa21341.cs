using System.Linq;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Cards
{
    public class DiceCardSelfAbility_ShadowSmallBuff_Sa21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBufList()
                       .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341)?.stack > 4 &&
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
            SkinUtil.BurnEffect(unit);
            if (unit.bufListDetail.GetActivatedBufList()
                    .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341) is BattleUnitBuf_Remembrance_Sa21341
                buff)
                buff.AddStacks(-5);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, unit);
            unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, unit);
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }
    }
}