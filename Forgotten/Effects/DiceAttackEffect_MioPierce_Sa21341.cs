using BigDLL4221.DiceEffects;

namespace VortexTower.Forgotten.Effects
{
    public class DiceAttackEffect_MioPierce_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.725f, 0.315f, 2.5f);
            base.Initialize(self, target, destroyTime);
        }
    }
}