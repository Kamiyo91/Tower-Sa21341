using VortexTower.Sae.Buffs;

namespace VortexTower.Sae.Passives
{
    public class PassiveAbility_DistortionBlessingPlayer_Sa21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            owner.RecoverHP(2);
        }

        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_DistortionBlessing_Sa21341());
        }

        public override void OnRoundEndTheLast()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_DistortionBlessing_Sa21341>())
                owner.bufListDetail.AddBuf(new BattleUnitBuf_DistortionBlessing_Sa21341());
        }
    }
}