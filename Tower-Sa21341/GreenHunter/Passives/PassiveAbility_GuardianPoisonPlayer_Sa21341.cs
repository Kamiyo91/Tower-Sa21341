using System.Linq;
using VortexLabyrinth_Sa21341.GreenHunter.Buffs;

namespace VortexLabyrinth_Sa21341.GreenHunter.Passives
{
    public class PassiveAbility_GuardianPoisonPlayer_Sa21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            var target = behavior.card.target;
            var poison = target.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_Poison_Sa21341);
            if (poison == null) target.bufListDetail.AddBuf(new BattleUnitBuf_Poison_Sa21341());
            else
                poison.stack++;
        }
    }
}