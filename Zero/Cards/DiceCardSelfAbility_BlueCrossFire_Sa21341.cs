using System.Linq;
using VortexTower.Zero.Buffs;

namespace VortexTower.Zero.Cards
{
    public class DiceCardSelfAbility_BlueCrossFire_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            if (!(owner.bufListDetail.GetActivatedBufList()
                        .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341) is BattleUnitBuf_BlueFlame_Sa21341
                    buff) || buff.stack <= 2) return;
            var dice = card.card.CreateDiceCardBehaviorList().FirstOrDefault();
            var limit = 0;
            for (var i = 0; i <= buff.stack + 3 && limit < 3; i += 3)
            {
                card.AddDice(dice);
                limit++;
                buff.AddStacks(-3);
            }
        }
    }
}