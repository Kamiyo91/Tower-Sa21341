using System.Linq;
using VortexTower.Zero.Buffs;

namespace VortexTower.Zero.Cards
{
    public class DiceCardSelfAbility_BlueFireBlade_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, null);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, null);
            if (owner.bufListDetail.GetActivatedBufList()
                    .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341) is BattleUnitBuf_BlueFlame_Sa21341 buff)
                buff.AddStacks(2);
        }
    }
}