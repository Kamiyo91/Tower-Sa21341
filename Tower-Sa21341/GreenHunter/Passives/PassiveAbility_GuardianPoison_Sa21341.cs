using System.Linq;

namespace VortexLabyrinth_Sa21341.GreenHunter.Passives
{
    public class PassiveAbility_GuardianPoison_Sa21341 : PassiveAbilityBase
    {
        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Decay, 1, owner);
            var targetBuff = RandomUtil.SelectOne(behavior.card.target.bufListDetail.GetActivatedBufList()
                .Where(x => x.positiveType == BufPositiveType.Positive).ToList());
            if (targetBuff.stack < 2) behavior.card.target.bufListDetail.RemoveBuf(targetBuff);
            else
                targetBuff.stack--;
        }
    }
}