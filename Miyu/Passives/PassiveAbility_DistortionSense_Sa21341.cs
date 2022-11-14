using BigDLL4221.Utils;

namespace VortexTower.Miyu.Passives
{
    public class PassiveAbility_DistortionSense_Sa21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, owner.faction == Faction.Player ? 39 : 25, VortexModParameters.PackageId);
        }
    }
}