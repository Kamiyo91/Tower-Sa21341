using VortexTower.Miyu.Buffs;

namespace VortexTower.Miyu.Passives
{
    public class PassiveAbility_CorruptedUniform_Sa21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_CorruptedUniform_Sa21341());
        }
    }
}