using System.Linq;
using VortexLabyrinth_Sa21341.Zero.Buffs;
using VortexLabyrinth_Sa21341.Zero.Dices;

namespace VortexLabyrinth_Sa21341.Zero.Cards
{
    public class DiceCardSelfAbility_BlueHorizon_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            var buff = owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341);
            if (buff == null || buff.stack < 3) return;
            buff.stack -= 3;
            owner.cardSlotDetail.RecoverPlayPoint(2);
            card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_BlueHorizonDice_Sa21341());
        }
    }
}