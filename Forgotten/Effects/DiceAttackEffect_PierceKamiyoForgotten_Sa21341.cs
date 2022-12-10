using BigDLL4221.DiceEffects;

namespace VortexTower.Forgotten.Effects
{
    public class DiceAttackEffect_PierceKamiyoForgotten_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.725f, 0.185f, 2.5f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }

    public class DiceAttackEffect_PierceKamiyoForgottenEnemy_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.725f, 0.185f, 2.5f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }
}