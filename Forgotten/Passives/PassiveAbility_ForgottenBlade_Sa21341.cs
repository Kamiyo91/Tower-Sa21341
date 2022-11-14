namespace VortexTower.Forgotten.Passives
{
    public class PassiveAbility_ForgottenBlade_Sa21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            owner.RecoverHP(1);
            owner.breakDetail.RecoverBreak(1);
        }
    }
}