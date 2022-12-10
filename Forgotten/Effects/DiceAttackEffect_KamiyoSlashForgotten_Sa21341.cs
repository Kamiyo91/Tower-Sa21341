using BigDLL4221.DiceEffects;

namespace VortexTower.Forgotten.Effects
{
    public class DiceAttackEffect_KamiyoSlashForgotten_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.55f, 0.15f, 2f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }

    public class DiceAttackEffect_KamiyoSlashForgottenEnemy_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.55f, 0.15f, 2f, fixedScale: true);
            base.Initialize(self, target, destroyTime);
        }
    }
}