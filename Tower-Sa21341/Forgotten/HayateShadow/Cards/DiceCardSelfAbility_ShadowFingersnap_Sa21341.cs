namespace VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Cards
{
    public class DiceCardSelfAbility_ShadowFingersnap_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnStartBattle()
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            foreach (var unit in BattleObjectManager.instance.GetAliveList(Faction.Player)) unit.TakeDamage(unit.MaxHp);
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