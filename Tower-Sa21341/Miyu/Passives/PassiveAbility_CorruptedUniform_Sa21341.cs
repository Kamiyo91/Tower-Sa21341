using VortexLabyrinth_Sa21341.Miyu.Buffs;

namespace VortexLabyrinth_Sa21341.Miyu.Passives
{
    public class PassiveAbility_CorruptedUniform_Sa21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_CorruptedUniform_Sa21341());
        }
    }
}