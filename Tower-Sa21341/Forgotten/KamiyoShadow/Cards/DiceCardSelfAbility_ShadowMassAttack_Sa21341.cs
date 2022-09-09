using System.Linq;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Cards
{
    public class DiceCardSelfAbility_ShadowMassAttack_Sa21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChanged;

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341)
                ?.stack > 9 && !owner.cardSlotDetail.cardAry.Exists(x =>
                x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 52) ||
                x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 69));
        }

        public override void OnUseCard()
        {
            var buff = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_Remembrance_Sa21341) as BattleUnitBuf_Remembrance_Sa21341;
            buff?.AddStacks(-10);
        }

        public override void OnEndAreaAttack()
        {
            if (!_motionChanged) return;
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnApplyCard()
        {
            if (!string.IsNullOrEmpty(owner.UnitData.unitData.workshopSkin) ||
                owner.UnitData.unitData.bookItem != owner.UnitData.unitData.CustomBookItem) return;
            _motionChanged = true;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Penetrate);
        }

        public override void OnReleaseCard()
        {
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }
    }
}