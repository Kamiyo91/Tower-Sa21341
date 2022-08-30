using System.Linq;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Cards
{
    public class DiceCardSelfAbility_ShadowMioSummon_Sa21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return UnitUtil.SupportCharCheck(owner) == 1 && owner.bufListDetail.GetActivatedBufList()
                       .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341)?.stack > -1 &&
                   !owner.cardSlotDetail.cardAry.Exists(x =>
                       x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 52) || x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 69));
        }
        public override void OnUseCard()
        {
            var buff = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341) as BattleUnitBuf_Remembrance_Sa21341;
            buff?.AddStacks(-15);
        }
    }
}
