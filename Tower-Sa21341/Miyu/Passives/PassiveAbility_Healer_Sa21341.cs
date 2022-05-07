using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.Miyu.Passives
{
    public class PassiveAbility_Healer_Sa21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            UnitUtil.ChangeLoneFixerPassive(owner.faction, new LorId(VortexModParameters.PackageId, 15));
            var passive = owner.passiveDetail.AddPassive(new PassiveAbility_251201());
            passive.Hide();
        }
    }
}