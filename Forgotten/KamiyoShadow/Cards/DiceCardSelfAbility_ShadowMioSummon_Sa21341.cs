using System.Linq;
using BigDLL4221.Utils;
using VortexTower.Forgotten.KamiyoShadow.Buffs;

namespace VortexTower.Forgotten.KamiyoShadow.Cards
{
    public class DiceCardSelfAbility_ShadowMioSummon_Sa21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return UnitUtil.SupportCharCheck(owner) == 1 && owner.bufListDetail.GetActivatedBufList()
                       .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341)?.stack > 9 &&
                   !owner.cardSlotDetail.cardAry.Exists(x =>
                       x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 52) ||
                       x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 69));
        }

        public override void OnUseCard()
        {
            var buff = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341) as BattleUnitBuf_Remembrance_Sa21341;
            buff?.AddStacks(-10);
        }
    }
}