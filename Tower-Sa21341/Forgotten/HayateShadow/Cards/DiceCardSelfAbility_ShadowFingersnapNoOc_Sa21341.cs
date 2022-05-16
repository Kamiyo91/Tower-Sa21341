using System.Linq;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.Miyu.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Cards
{
    public class DiceCardSelfAbility_ShadowFingersnapNoOc_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }

        public override void OnUseCard()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(UnitUtil.ReturnOtherSideFaction(owner.faction))
                         .Where(x => !x.bufListDetail.HasBuf<BattleUnitBuf_BlueShield_Sa21341>())) unit.TakeDamage(40);
        }

        public override void OnApplyCard()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Aim);
        }

        public override void OnReleaseCard()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
        }
    }
}