using System.Linq;
using VortexLabyrinth_Sa21341.Zero.Buffs;

namespace VortexLabyrinth_Sa21341.Zero.Cards
{
    public class DiceCardSelfAbility_BlueCrossFire_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            var buff = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341);
            if (buff == null || buff.stack <= 2) return;
            var dice = card.card.CreateDiceCardBehaviorList().FirstOrDefault();
            var limit = 0;
            for (var i = 0; i <= buff.stack + 3 && limit < 3; i += 3)
            {
                card.AddDice(dice);
                limit++;
                buff.stack -= 3;
            }
        }
    }
}