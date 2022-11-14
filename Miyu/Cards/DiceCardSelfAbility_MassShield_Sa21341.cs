using VortexTower.Miyu.Buffs;

namespace VortexTower.Miyu.Cards
{
    public class DiceCardSelfAbility_MassShield_Sa21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChanged;

        public override string[] Keywords
        {
            get
            {
                return new[]
                {
                    "YellowShield_Sa21341"
                };
            }
        }

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return !owner.cardSlotDetail.cardAry.Exists(x =>
                x?.card?.GetID() == new LorId(VortexModParameters.PackageId, 15));
        }

        public override void OnStartBattle()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction))
            {
                unit.bufListDetail.GetActivatedBufList().RemoveAll(x => x.positiveType == BufPositiveType.Negative);
                if (!unit.bufListDetail.HasBuf<BattleUnitBuf_YellowShield_Sa21341>())
                    unit.bufListDetail.AddBuf(new BattleUnitBuf_YellowShield_Sa21341());
            }
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
            owner.view.charAppearance.ChangeMotion(ActionDetail.Evade);
        }

        public override void OnReleaseCard()
        {
            _motionChanged = false;
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }
    }
}