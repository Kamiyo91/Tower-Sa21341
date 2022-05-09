using System.Linq;
using VortexLabyrinth_Sa21341.Zero.Buffs;

namespace VortexLabyrinth_Sa21341.Zero.Cards
{
    public class DiceCardSelfAbility_BlueFireBlade_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, null);
            owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, null);
            var buff = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341);
            if (buff != null)
                buff.stack += 2;
        }
    }
}