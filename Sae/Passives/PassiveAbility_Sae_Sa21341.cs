using VortexTower.Sae.Buffs;

namespace VortexTower.Sae.Passives
{
    public class PassiveAbility_Sae_Sa21341 : PassiveAbilityBase
    {
        public override void OnWaveStart()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_RedAura_Sa21341>())
                owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_RedAura_Sa21341());
        }

        public override float GetStartHp(float hp)
        {
            return hp * 0.25f;
        }
    }
}