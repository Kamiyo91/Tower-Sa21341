using BigDLL4221.DiceEffects;

namespace VortexTower.Sae.Effects
{
    public class DiceAttackEffect_SaeHit_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.54f, 0.35f, 2.15f);
            base.Initialize(self, target, destroyTime);
        }
    }
}