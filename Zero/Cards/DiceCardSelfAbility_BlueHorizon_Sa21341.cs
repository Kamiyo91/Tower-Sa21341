using System.Linq;
using VortexTower.Zero.Buffs;
using VortexTower.Zero.Dice;

namespace VortexTower.Zero.Cards
{
    public class DiceCardSelfAbility_BlueHorizon_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseCard()
        {
            owner.allyCardDetail.DrawCards(1);
            if (!(owner.bufListDetail.GetActivatedBufList()
                        .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341) is BattleUnitBuf_BlueFlame_Sa21341
                    buff) || buff.stack < 3) return;
            buff.AddStacks(-3);
            owner.cardSlotDetail.RecoverPlayPoint(2);
            card.ApplyDiceAbility(DiceMatch.AllDice, new DiceCardAbility_BlueHorizonDice_Sa21341());
        }
    }
}