using System.Linq;
using VortexLabyrinth_Sa21341.Zero.Buffs;

namespace VortexLabyrinth_Sa21341.Zero.Cards
{
    public class DiceCardSelfAbility_BlueFireMassAttack_Sa21341 : DiceCardSelfAbilityBase
    {
        private bool _motionChanged;

        public override void OnUseCard()
        {
            var buff = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341);
            if (buff == null || buff.stack <= 19) return;
            var dice = card.card.CreateDiceCardBehaviorList().FirstOrDefault();
            card.AddDice(dice);
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                power = 8
            });
            buff.stack = 0;
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