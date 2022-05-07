using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.Miyu.Passives
{
    public class PassiveAbility_DistortionSense_Sa21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 26, VortexModParameters.PackageId);
        }
    }
}