using BigDLL4221.DiceEffects;

namespace VortexTower.Sae.Effects
{
    public class DiceAttackEffect_DarkSaeSlash_Sa21341 : DiceAttackEffect_BaseAttackEffect_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, 0.5f, 0.2f, 5f);
            base.Initialize(self, target, destroyTime);
        }
    }
}