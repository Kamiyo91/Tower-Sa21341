using BigDLL4221.DiceEffects;

namespace VortexTower.Forgotten.Effects
{
    public class DiceAttackEffect_KamiyoHitForgotten_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.54f, 0.32f, 2.5f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }

    public class DiceAttackEffect_KamiyoHitForgottenEnemy_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.54f, 0.32f, 2.5f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }
}