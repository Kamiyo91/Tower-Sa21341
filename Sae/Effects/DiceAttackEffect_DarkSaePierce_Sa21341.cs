using BigDLL4221.DiceEffects;

namespace VortexTower.Sae.Effects
{
    public class DiceAttackEffect_DarkSaePierce_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.725f, 0.375f, 2.5f);
            base.Initialize(self, target, destroyTime);
        }
    }
}