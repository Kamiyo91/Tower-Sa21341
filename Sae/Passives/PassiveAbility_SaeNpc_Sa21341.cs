using BigDLL4221.Passives;
using VortexTower.Sae.Dice;

namespace VortexTower.Sae.Passives
{
    public class PassiveAbility_SaeNpc_Sa21341 : PassiveAbility_NpcMechBase_DLL4221
    {
        public bool IsImmortal;
        public override bool isImmortal => IsImmortal;

        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new SaeUtil().SaeNpcUtil);
            IsImmortal = StageController.Instance.GetStageModel().ClassInfo.id.id == 1;
        }

        public override float GetStartHp(float hp)
        {
            return hp * 0.50f;
        }

        public void SetImmortal(bool value)
        {
            IsImmortal = value;
        }

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            if (!atkDice.abilityList.Exists(x => x is DiceCardAbility_RagingDeathDice_Sa21341) || owner.hp - dmg > 1)
            {
                base.OnTakeDamageByAttack(atkDice, dmg);
                return;
            }

            owner.Die();
        }
    }
}