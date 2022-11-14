using BigDLL4221.Passives;

namespace VortexTower.Miyu.Passives
{
    public class PassiveAbility_Healer_Sa21341 : PassiveAbility_SupportChar_DLL4221
    {
        public override void OnWaveStart()
        {
            var passive = owner.passiveDetail.AddPassive(new PassiveAbility_251201());
            passive.Hide();
        }
    }
}