using BigDLL4221.DiceEffects;

namespace VortexTower.Forgotten.Effects
{
    public class DiceAttackEffect_Hit_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.54f, 0.23f, 2.5f);
            base.Initialize(self, target, destroyTime);
        }
    }
}