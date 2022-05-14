using System.Linq;
using VortexLabyrinth_Sa21341.Miyu.Passives;
using VortexLabyrinth_Sa21341.Sae.Buffs;

namespace VortexLabyrinth_Sa21341.Sae.Passives
{
    public class PassiveAbility_DistortionBlessing_Sa21341 : PassiveAbilityBase
    {
        private BattleUnitModel _miyuUnit;

        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_DistortionBlessing_Sa21341());
            _miyuUnit = BattleObjectManager.instance.GetAliveList(owner.faction)
                .FirstOrDefault(x => x.passiveDetail.HasPassive<PassiveAbility_Miyu_Sa21341>());
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                power = 1
            });
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            _miyuUnit?.RecoverHP(4);
        }
    }
}