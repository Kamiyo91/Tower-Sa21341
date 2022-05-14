using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Passives
{
    public class PassiveAbility_ForgottenSense_Sa21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 18, VortexModParameters.PackageId);
        }
    }
}