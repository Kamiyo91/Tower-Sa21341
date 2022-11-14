using System.Linq;
using VortexTower.Zero.GreenHunter.Buffs;

namespace VortexTower.Zero.GreenHunter.Passives
{
    public class PassiveAbility_GuardianPoison_Sa21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            var target = behavior.card.target;
            var poison = target.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_Poison_Sa21341);
            if (poison == null) target.bufListDetail.AddBuf(new BattleUnitBuf_Poison_Sa21341());
            else
                poison.stack++;
            var targetBuffs = target.bufListDetail.GetActivatedBufList()
                .Where(x => x.positiveType == BufPositiveType.Positive).ToList();
            if (!targetBuffs.Any()) return;
            var targetBuff = RandomUtil.SelectOne(targetBuffs);
            if (targetBuff.stack < 2) target.bufListDetail.RemoveBuf(targetBuff);
            else
                targetBuff.stack--;
        }
    }
}