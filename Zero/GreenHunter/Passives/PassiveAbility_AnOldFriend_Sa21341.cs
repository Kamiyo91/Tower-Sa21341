using System.Linq;
using VortexTower.Zero.Passives;

namespace VortexTower.Zero.GreenHunter.Passives
{
    public class PassiveAbility_AnOldFriend_Sa21341 : PassiveAbilityBase
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(!BattleObjectManager.instance.GetAliveList(owner.faction).Exists(x =>
                x.passiveDetail.PassiveList.Exists(y => y is PassiveAbility_Zero_Sa21341))
                ? new DiceStatBonus { max = 1 }
                : new DiceStatBonus { min = 1 });
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            var unit = BattleObjectManager.instance.GetAliveList(owner.faction).FirstOrDefault(x =>
                x.passiveDetail.PassiveList.Exists(y => y is PassiveAbility_Zero_Sa21341));
            if (unit == null)
            {
                owner.breakDetail.RecoverBreak(2);
            }
            else
            {
                owner.RecoverHP(2);
                unit.RecoverHP(2);
            }
        }
    }
}