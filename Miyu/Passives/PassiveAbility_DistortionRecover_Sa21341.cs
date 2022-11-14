namespace VortexTower.Miyu.Passives
{
    public class PassiveAbility_DistortionRecover_Sa21341 : PassiveAbilityBase
    {
        public override void OnRoundEnd()
        {
            owner.RecoverHP(10);
            owner.breakDetail.RecoverBreak(10);
        }
    }
}