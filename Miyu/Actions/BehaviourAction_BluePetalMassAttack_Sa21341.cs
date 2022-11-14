using BigDLL4221.FarAreaEffects;

namespace VortexTower.Miyu.Actions
{
    public class BehaviourAction_BluePetalMassAttack_Sa21341 : BehaviourAction_BaseMassAttackEffect_DLL4221
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            SetParameters(typeof(FarAreaEffect_BluePetalMassAttack_Sa21341));
            return base.SetFarAreaAtkEffect(self);
        }
    }

    public class FarAreaEffect_BluePetalMassAttack_Sa21341 : FarAreaEffect_BaseMassAttackEffect_DLL4221
    {
        public override void Init(BattleUnitModel self, params object[] args)
        {
            SetParameters(ActionDetail.Fire, "BluePetalWindAttack21341.ogg", "BluePetalMassAttack_Sa21341", zoom: false,
                characterMove: false, followUnits: false);
            base.Init(self, args);
        }
    }

    public class DiceAttackEffect_BluePetalMassAttack_Sa21341 : DiceAttackEffect_BaseAreaAtk_DLL4221
    {
        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            SetParameters(VortexModParameters.Path, scale: 3f);
            base.Initialize(self, target, destroyTime);
        }
    }
}