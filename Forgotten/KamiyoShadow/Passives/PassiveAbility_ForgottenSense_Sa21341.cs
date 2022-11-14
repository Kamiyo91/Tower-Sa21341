using BigDLL4221.Utils;

namespace VortexTower.Forgotten.KamiyoShadow.Passives
{
    public class PassiveAbility_ForgottenSense_Sa21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 63, VortexModParameters.PackageId);
        }
    }
}